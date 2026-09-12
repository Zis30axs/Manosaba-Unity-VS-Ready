using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Il2CppDummyDll;
using Naninovel.Async;
using Naninovel.Async.CompilerServices;

namespace Naninovel;

[Token(Token = "0x20001D8")]
public static class UniTaskExtensions
{
	[Token(Token = "0x20001D9")]
	private class ToCoroutineEnumerator : IEnumerator
	{
		[StructLayout(3)]
		[Token(Token = "0x20001DA")]
		[CompilerGenerated]
		private struct _003CRunTask_003Ed__6
{
			[Token(Token = "0x40007B6")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40007B7")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40007B8")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
			public UniTask task;

			[Token(Token = "0x40007B9")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
			public ToCoroutineEnumerator _003C_003E4__this;

			[Token(Token = "0x40007BA")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
			private UniTask.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000869")]
			[Address(RVA = "0x51BBD0", Offset = "0x51A5D0", VA = "0x18051BBD0", Slot = "4")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x600086A")]
			[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x40007B1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		private bool completed;

		[Token(Token = "0x40007B2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		private readonly UniTask task;

		[Token(Token = "0x40007B3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private readonly Action<Exception> exceptionHandler;

		[Token(Token = "0x40007B4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private bool isStarted;

		[Token(Token = "0x40007B5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x17000132")]
		public object Current
		{
			[Token(Token = "0x6000866")]
			[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0", Slot = "5")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x6000864")]
		[Address(RVA = "0x51B7D0", Offset = "0x51A1D0", VA = "0x18051B7D0")]
		public ToCoroutineEnumerator(UniTask task, Action<Exception> exceptionHandler)
		{
		}

		[Token(Token = "0x6000865")]
		[Address(RVA = "0x51B880", Offset = "0x51A280", VA = "0x18051B880")]
		[AsyncStateMachine(typeof(_003CRunTask_003Ed__6))]
		private UniTaskVoid RunTask(UniTask task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000867")]
		[Address(RVA = "0x51B9F0", Offset = "0x51A3F0", VA = "0x18051B9F0", Slot = "4")]
		public bool MoveNext()
		{
			return default(bool);
		}

		[Token(Token = "0x6000868")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "6")]
		public void Reset()
		{
		}
	}

	[Token(Token = "0x20001DB")]
	private class ToCoroutineEnumerator<T> : IEnumerator
	{
		[StructLayout(3)]
		[Token(Token = "0x20001DC")]
		[CompilerGenerated]
		private struct _003CRunTask_003Ed__8
{
			[Token(Token = "0x40007C2")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40007C3")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40007C4")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T> task;

			[Token(Token = "0x40007C5")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public ToCoroutineEnumerator<T> _003C_003E4__this;

			[Token(Token = "0x40007C6")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000870")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000871")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x40007BB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private bool completed;

		[Token(Token = "0x40007BC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private readonly Action<T> resultHandler;

		[Token(Token = "0x40007BD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private readonly Action<Exception> exceptionHandler;

		[Token(Token = "0x40007BE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private bool isStarted;

		[Token(Token = "0x40007BF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private readonly UniTask<T> task;

		[Token(Token = "0x40007C0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private object current;

		[Token(Token = "0x40007C1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x17000133")]
		public object Current
		{
			[Token(Token = "0x600086D")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x600086B")]
		public ToCoroutineEnumerator(UniTask<T> task, Action<T> resultHandler, Action<Exception> exceptionHandler)
		{
		}

		[Token(Token = "0x600086C")]
		[AsyncStateMachine(typeof(ToCoroutineEnumerator<>._003CRunTask_003Ed__8))]
		private UniTaskVoid RunTask(UniTask<T> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x600086E")]
		public bool MoveNext()
		{
			return default(bool);
		}

		[Token(Token = "0x600086F")]
		public void Reset()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001DF")]
	[CompilerGenerated]
	private struct _003CConfigureAwait_003Ed__25
{
		[Token(Token = "0x40007CB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40007CC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40007CD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public Task task;

		[Token(Token = "0x40007CE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public PlayerLoopTiming timing;

		[Token(Token = "0x40007CF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

		[Token(Token = "0x40007D0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private YieldAwaitable.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000878")]
		[Address(RVA = "0x51C2D0", Offset = "0x51ACD0", VA = "0x18051C2D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000879")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001E0")]
	[CompilerGenerated]
	private struct _003CConfigureAwait_003Ed__26<T>
{
		[Token(Token = "0x40007D1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40007D2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<T> _003C_003Et__builder;

		[Token(Token = "0x40007D3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Task<T> task;

		[Token(Token = "0x40007D4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public PlayerLoopTiming timing;

		[Token(Token = "0x40007D5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T _003Cv_003E5__2;

		[Token(Token = "0x40007D6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ConfiguredTaskAwaitable<T>.ConfiguredTaskAwaiter _003C_003Eu__1;

		[Token(Token = "0x40007D7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private YieldAwaitable.Awaiter _003C_003Eu__2;

		[Token(Token = "0x600087A")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600087B")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001E1")]
	[CompilerGenerated]
	private struct _003CConfigureAwait_003Ed__27
{
		[Token(Token = "0x40007D8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40007D9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40007DA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public UniTask task;

		[Token(Token = "0x40007DB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public PlayerLoopTiming timing;

		[Token(Token = "0x40007DC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40007DD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private YieldAwaitable.Awaiter _003C_003Eu__2;

		[Token(Token = "0x600087C")]
		[Address(RVA = "0x51C660", Offset = "0x51B060", VA = "0x18051C660", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600087D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001E2")]
	[CompilerGenerated]
	private struct _003CConfigureAwait_003Ed__28<T>
{
		[Token(Token = "0x40007DE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40007DF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<T> _003C_003Et__builder;

		[Token(Token = "0x40007E0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T> task;

		[Token(Token = "0x40007E1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public PlayerLoopTiming timing;

		[Token(Token = "0x40007E2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T _003Cv_003E5__2;

		[Token(Token = "0x40007E3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<T>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40007E4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private YieldAwaitable.Awaiter _003C_003Eu__2;

		[Token(Token = "0x600087E")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600087F")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001E3")]
	[CompilerGenerated]
	private struct _003CContinueWith_003Ed__17<T>
{
		[Token(Token = "0x40007E5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40007E6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40007E7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Action<T> continuationFunction;

		[Token(Token = "0x40007E8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T> task;

		[Token(Token = "0x40007E9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Action<T> _003C_003E7__wrap1;

		[Token(Token = "0x40007EA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<T>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000880")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000881")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001E4")]
	[CompilerGenerated]
	private struct _003CContinueWith_003Ed__18<T>
{
		[Token(Token = "0x40007EB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40007EC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40007ED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Func<T, UniTask> continuationFunction;

		[Token(Token = "0x40007EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T> task;

		[Token(Token = "0x40007EF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Func<T, UniTask> _003C_003E7__wrap1;

		[Token(Token = "0x40007F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<T>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40007F1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000882")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000883")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001E5")]
	[CompilerGenerated]
	private struct _003CContinueWith_003Ed__19<T, TR>
{
		[Token(Token = "0x40007F2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40007F3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<TR> _003C_003Et__builder;

		[Token(Token = "0x40007F4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Func<T, TR> continuationFunction;

		[Token(Token = "0x40007F5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T> task;

		[Token(Token = "0x40007F6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Func<T, TR> _003C_003E7__wrap1;

		[Token(Token = "0x40007F7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<T>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000884")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000885")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001E6")]
	[CompilerGenerated]
	private struct _003CContinueWith_003Ed__20<T, TR>
{
		[Token(Token = "0x40007F8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40007F9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<TR> _003C_003Et__builder;

		[Token(Token = "0x40007FA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Func<T, UniTask<TR>> continuationFunction;

		[Token(Token = "0x40007FB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T> task;

		[Token(Token = "0x40007FC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Func<T, UniTask<TR>> _003C_003E7__wrap1;

		[Token(Token = "0x40007FD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<T>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40007FE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<TR>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000886")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000887")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001E7")]
	[CompilerGenerated]
	private struct _003CContinueWith_003Ed__21
{
		[Token(Token = "0x40007FF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000800")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000801")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public UniTask task;

		[Token(Token = "0x4000802")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Action continuationFunction;

		[Token(Token = "0x4000803")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000888")]
		[Address(RVA = "0x51CA40", Offset = "0x51B440", VA = "0x18051CA40", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000889")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001E8")]
	[CompilerGenerated]
	private struct _003CContinueWith_003Ed__22
{
		[Token(Token = "0x4000804")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000805")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000806")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public UniTask task;

		[Token(Token = "0x4000807")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Func<UniTask> continuationFunction;

		[Token(Token = "0x4000808")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600088A")]
		[Address(RVA = "0x51CCF0", Offset = "0x51B6F0", VA = "0x18051CCF0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600088B")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001E9")]
	[CompilerGenerated]
	private struct _003CContinueWith_003Ed__23<T>
{
		[Token(Token = "0x4000809")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400080A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<T> _003C_003Et__builder;

		[Token(Token = "0x400080B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask task;

		[Token(Token = "0x400080C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Func<T> continuationFunction;

		[Token(Token = "0x400080D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600088C")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600088D")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001EA")]
	[CompilerGenerated]
	private struct _003CContinueWith_003Ed__24<T>
{
		[Token(Token = "0x400080E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400080F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<T> _003C_003Et__builder;

		[Token(Token = "0x4000810")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask task;

		[Token(Token = "0x4000811")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Func<UniTask<T>> continuationFunction;

		[Token(Token = "0x4000812")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000813")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<T>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x600088E")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600088F")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001EB")]
	[CompilerGenerated]
	private struct _003CForgetCore_003Ed__11
{
		[Token(Token = "0x4000814")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000815")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000816")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public UniTask task;

		[Token(Token = "0x4000817")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000890")]
		[Address(RVA = "0x51D110", Offset = "0x51BB10", VA = "0x18051D110", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000891")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001EC")]
	[CompilerGenerated]
	private struct _003CForgetCore_003Ed__15<T>
{
		[Token(Token = "0x4000818")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000819")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400081A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T> task;

		[Token(Token = "0x400081B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<T>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000892")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000893")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001ED")]
	[CompilerGenerated]
	private struct _003CForgetCoreWithCatch_003Ed__12
{
		[Token(Token = "0x400081C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400081D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400081E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public UniTask task;

		[Token(Token = "0x400081F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public bool handleExceptionOnMainThread;

		[Token(Token = "0x4000820")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Action<Exception> exceptionHandler;

		[Token(Token = "0x4000821")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private object _003C_003E7__wrap1;

		[Token(Token = "0x4000822")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private int _003C_003E7__wrap2;

		[Token(Token = "0x4000823")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000824")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private Exception _003Cex_003E5__4;

		[Token(Token = "0x4000825")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private SwitchToMainThreadAwaitable.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000894")]
		[Address(RVA = "0x51D390", Offset = "0x51BD90", VA = "0x18051D390", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000895")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001EE")]
	[CompilerGenerated]
	private struct _003CForgetCoreWithCatch_003Ed__16<T>
{
		[Token(Token = "0x4000826")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000827")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000828")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T> task;

		[Token(Token = "0x4000829")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public bool handleExceptionOnMainThread;

		[Token(Token = "0x400082A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Action<Exception> exceptionHandler;

		[Token(Token = "0x400082B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private object _003C_003E7__wrap1;

		[Token(Token = "0x400082C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int _003C_003E7__wrap2;

		[Token(Token = "0x400082D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<T>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400082E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Exception _003Cex_003E5__4;

		[Token(Token = "0x400082F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private SwitchToMainThreadAwaitable.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000896")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000897")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001EF")]
	[CompilerGenerated]
	private struct _003CTimeout_003Ed__6<T>
{
		[Token(Token = "0x4000830")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000831")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<T> _003C_003Et__builder;

		[Token(Token = "0x4000832")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public TimeSpan timeout;

		[Token(Token = "0x4000833")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public bool ignoreTimeScale;

		[Token(Token = "0x4000834")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public PlayerLoopTiming timeoutCheckTiming;

		[Token(Token = "0x4000835")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T> task;

		[Token(Token = "0x4000836")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public CancellationTokenSource taskCancellationTokenSource;

		[Token(Token = "0x4000837")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private CancellationTokenSource _003CdelayCancellationTokenSource_003E5__2;

		[Token(Token = "0x4000838")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<(bool hasResultLeft, (bool IsCanceled, T Result) result)>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000898")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000899")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001F0")]
	[CompilerGenerated]
	private struct _003CTimeoutWithoutException_003Ed__7
{
		[Token(Token = "0x4000839")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400083A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

		[Token(Token = "0x400083B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public UniTask task;

		[Token(Token = "0x400083C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public TimeSpan timeout;

		[Token(Token = "0x400083D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public bool ignoreTimeScale;

		[Token(Token = "0x400083E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
		public PlayerLoopTiming timeoutCheckTiming;

		[Token(Token = "0x400083F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public CancellationTokenSource taskCancellationTokenSource;

		[Token(Token = "0x4000840")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask<(bool IsTimeout, AsyncUnit Result)>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600089A")]
		[Address(RVA = "0x51D950", Offset = "0x51C350", VA = "0x18051D950", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600089B")]
		[Address(RVA = "0x51DC10", Offset = "0x51C610", VA = "0x18051DC10", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001F1")]
	[CompilerGenerated]
	private struct _003CTimeoutWithoutException_003Ed__8<T>
{
		[Token(Token = "0x4000841")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000842")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<(bool IsTimeout, T Result)> _003C_003Et__builder;

		[Token(Token = "0x4000843")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public TimeSpan timeout;

		[Token(Token = "0x4000844")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public bool ignoreTimeScale;

		[Token(Token = "0x4000845")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public PlayerLoopTiming timeoutCheckTiming;

		[Token(Token = "0x4000846")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T> task;

		[Token(Token = "0x4000847")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public CancellationTokenSource taskCancellationTokenSource;

		[Token(Token = "0x4000848")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private CancellationTokenSource _003CdelayCancellationTokenSource_003E5__2;

		[Token(Token = "0x4000849")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<(bool hasResultLeft, (bool IsCanceled, T Result) result)>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600089C")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600089D")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001F2")]
	[CompilerGenerated]
	private struct _003CUnwrap_003Ed__29<T>
{
		[Token(Token = "0x400084A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400084B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<T> _003C_003Et__builder;

		[Token(Token = "0x400084C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<UniTask<T>> task;

		[Token(Token = "0x400084D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<UniTask<T>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400084E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<T>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x600089E")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600089F")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001F3")]
	[CompilerGenerated]
	private struct _003CUnwrap_003Ed__30<T>
{
		[Token(Token = "0x400084F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000850")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000851")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<UniTask> task;

		[Token(Token = "0x4000852")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<UniTask>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000853")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60008A0")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60008A1")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x600083D")]
	[Address(RVA = "0x51A520", Offset = "0x518F20", VA = "0x18051A520")]
	public static UniTask<AsyncUnit> AsAsyncUnitUniTask(this UniTask task)
	{
		return default(UniTask<AsyncUnit>);
	}

	[Token(Token = "0x600083E")]
	public static UniTask<T> AsUniTask<T>(this Task<T> task, bool useCurrentSynchronizationContext = true)
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x600083F")]
	[Address(RVA = "0x51A6E0", Offset = "0x5190E0", VA = "0x18051A6E0")]
	public static UniTask AsUniTask(this Task task, bool useCurrentSynchronizationContext = true)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000840")]
	public static IEnumerator ToCoroutine<T>(this UniTask<T> task, Action<T> resultHandler = default, Action<Exception> exceptionHandler = default)
	{
		return null;
	}

	[Token(Token = "0x6000841")]
	[Address(RVA = "0x51A950", Offset = "0x519350", VA = "0x18051A950")]
	public static IEnumerator ToCoroutine(this UniTask task, Action<Exception> exceptionHandler = default)
	{
		return null;
	}

	[Token(Token = "0x6000842")]
	[Address(RVA = "0x51AA50", Offset = "0x519450", VA = "0x18051AA50")]
	public static UniTask Timeout(this UniTask task, TimeSpan timeout, bool ignoreTimeScale = true, PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update, CancellationTokenSource taskCancellationTokenSource = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000843")]
	[AsyncStateMachine(typeof(_003CTimeout_003Ed__6<>))]
	public static UniTask<T> Timeout<T>(this UniTask<T> task, TimeSpan timeout, bool ignoreTimeScale = true, PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update, CancellationTokenSource taskCancellationTokenSource = default)
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x6000844")]
	[Address(RVA = "0x51AB30", Offset = "0x519530", VA = "0x18051AB30")]
	[AsyncStateMachine(typeof(_003CTimeoutWithoutException_003Ed__7))]
	public static UniTask<bool> TimeoutWithoutException(this UniTask task, TimeSpan timeout, bool ignoreTimeScale = true, PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update, CancellationTokenSource taskCancellationTokenSource = default)
	{
		return default(UniTask<bool>);
	}

	[Token(Token = "0x6000845")]
	[AsyncStateMachine(typeof(_003CTimeoutWithoutException_003Ed__8<>))]
	public static UniTask<(bool, T)> TimeoutWithoutException<T>(this UniTask<T> task, TimeSpan timeout, bool ignoreTimeScale = true, PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update, CancellationTokenSource taskCancellationTokenSource = default)
	{
		return default(UniTask<(bool, T)>);
	}

	[Token(Token = "0x6000846")]
	[Address(RVA = "0x51AD40", Offset = "0x519740", VA = "0x18051AD40")]
	public static void Forget(this UniTask task)
	{
	}

	[Token(Token = "0x6000847")]
	[Address(RVA = "0x51AD50", Offset = "0x519750", VA = "0x18051AD50")]
	public static void Forget(this UniTask task, Action<Exception> exceptionHandler, bool handleExceptionOnMainThread = true)
	{
	}

	[Token(Token = "0x6000848")]
	[Address(RVA = "0x51AF10", Offset = "0x519910", VA = "0x18051AF10")]
	[AsyncStateMachine(typeof(_003CForgetCore_003Ed__11))]
	private static UniTaskVoid ForgetCore(UniTask task)
	{
		return default(UniTaskVoid);
	}

	[Token(Token = "0x6000849")]
	[Address(RVA = "0x51B020", Offset = "0x519A20", VA = "0x18051B020")]
	[AsyncStateMachine(typeof(_003CForgetCoreWithCatch_003Ed__12))]
	private static UniTaskVoid ForgetCoreWithCatch(UniTask task, Action<Exception> exceptionHandler, bool handleExceptionOnMainThread)
	{
		return default(UniTaskVoid);
	}

	[Token(Token = "0x600084A")]
	public static void Forget<T>(this UniTask<T> task)
	{
	}

	[Token(Token = "0x600084B")]
	public static void Forget<T>(this UniTask<T> task, Action<Exception> exceptionHandler, bool handleExceptionOnMainThread = true)
	{
	}

	[Token(Token = "0x600084C")]
	[AsyncStateMachine(typeof(_003CForgetCore_003Ed__15<>))]
	private static UniTaskVoid ForgetCore<T>(UniTask<T> task)
	{
		return default(UniTaskVoid);
	}

	[Token(Token = "0x600084D")]
	[AsyncStateMachine(typeof(_003CForgetCoreWithCatch_003Ed__16<>))]
	private static UniTaskVoid ForgetCoreWithCatch<T>(UniTask<T> task, Action<Exception> exceptionHandler, bool handleExceptionOnMainThread)
	{
		return default(UniTaskVoid);
	}

	[Token(Token = "0x600084E")]
	[AsyncStateMachine(typeof(_003CContinueWith_003Ed__17<>))]
	public static UniTask ContinueWith<T>(this UniTask<T> task, Action<T> continuationFunction)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600084F")]
	[AsyncStateMachine(typeof(_003CContinueWith_003Ed__18<>))]
	public static UniTask ContinueWith<T>(this UniTask<T> task, Func<T, UniTask> continuationFunction)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000850")]
	[AsyncStateMachine(typeof(_003CContinueWith_003Ed__19<, >))]
	public static UniTask<TR> ContinueWith<T, TR>(this UniTask<T> task, Func<T, TR> continuationFunction)
	{
		return default(UniTask<TR>);
	}

	[Token(Token = "0x6000851")]
	[AsyncStateMachine(typeof(_003CContinueWith_003Ed__20<, >))]
	public static UniTask<TR> ContinueWith<T, TR>(this UniTask<T> task, Func<T, UniTask<TR>> continuationFunction)
	{
		return default(UniTask<TR>);
	}

	[Token(Token = "0x6000852")]
	[Address(RVA = "0x51B1B0", Offset = "0x519BB0", VA = "0x18051B1B0")]
	[AsyncStateMachine(typeof(_003CContinueWith_003Ed__21))]
	public static UniTask ContinueWith(this UniTask task, Action continuationFunction)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000853")]
	[Address(RVA = "0x51B330", Offset = "0x519D30", VA = "0x18051B330")]
	[AsyncStateMachine(typeof(_003CContinueWith_003Ed__22))]
	public static UniTask ContinueWith(this UniTask task, Func<UniTask> continuationFunction)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000854")]
	[AsyncStateMachine(typeof(_003CContinueWith_003Ed__23<>))]
	public static UniTask<T> ContinueWith<T>(this UniTask task, Func<T> continuationFunction)
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x6000855")]
	[AsyncStateMachine(typeof(_003CContinueWith_003Ed__24<>))]
	public static UniTask<T> ContinueWith<T>(this UniTask task, Func<UniTask<T>> continuationFunction)
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x6000856")]
	[Address(RVA = "0x51B4B0", Offset = "0x519EB0", VA = "0x18051B4B0")]
	[AsyncStateMachine(typeof(_003CConfigureAwait_003Ed__25))]
	public static UniTask ConfigureAwait(this Task task, PlayerLoopTiming timing)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000857")]
	[AsyncStateMachine(typeof(_003CConfigureAwait_003Ed__26<>))]
	public static UniTask<T> ConfigureAwait<T>(this Task<T> task, PlayerLoopTiming timing)
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x6000858")]
	[Address(RVA = "0x51B5F0", Offset = "0x519FF0", VA = "0x18051B5F0")]
	[AsyncStateMachine(typeof(_003CConfigureAwait_003Ed__27))]
	public static UniTask ConfigureAwait(this UniTask task, PlayerLoopTiming timing)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000859")]
	[AsyncStateMachine(typeof(_003CConfigureAwait_003Ed__28<>))]
	public static UniTask<T> ConfigureAwait<T>(this UniTask<T> task, PlayerLoopTiming timing)
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x600085A")]
	[AsyncStateMachine(typeof(_003CUnwrap_003Ed__29<>))]
	public static UniTask<T> Unwrap<T>(this UniTask<UniTask<T>> task)
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x600085B")]
	[AsyncStateMachine(typeof(_003CUnwrap_003Ed__30<>))]
	public static UniTask Unwrap<T>(this UniTask<UniTask> task)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600085C")]
	[Address(RVA = "0x51B730", Offset = "0x51A130", VA = "0x18051B730")]
	public static UniTask.Awaiter GetAwaiter(this IEnumerable<UniTask> tasks)
	{
		return default(UniTask.Awaiter);
	}

	[Token(Token = "0x600085D")]
	public static UniTask<T[]>.Awaiter GetAwaiter<T>(this IEnumerable<UniTask<T>> tasks)
	{
		return default(UniTask<T[]>.Awaiter);
	}

	[Token(Token = "0x600085E")]
	public static UniTask<(T1, T2)>.Awaiter GetAwaiter<T1, T2>(this (UniTask<T1> task1, UniTask<T2> task2) tasks)
	{
		return default(UniTask<(T1, T2)>.Awaiter);
	}

	[Token(Token = "0x600085F")]
	public static UniTask<(T1, T2, T3)> WhenAll<T1, T2, T3>(this (UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3) tasks)
	{
		return default(UniTask<(T1, T2, T3)>);
	}

	[Token(Token = "0x6000860")]
	public static UniTask<(T1, T2, T3, T4)> WhenAll<T1, T2, T3, T4>(this (UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4) tasks)
	{
		return default(UniTask<(T1, T2, T3, T4)>);
	}

	[Token(Token = "0x6000861")]
	public static UniTask<(T1, T2, T3, T4, T5)> WhenAll<T1, T2, T3, T4, T5>(this (UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5) tasks)
	{
		return default(UniTask<(T1, T2, T3, T4, T5)>);
	}

	[Token(Token = "0x6000862")]
	public static UniTask<(T1, T2, T3, T4, T5, T6)> WhenAll<T1, T2, T3, T4, T5, T6>(this (UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6) tasks)
	{
		return default(UniTask<(T1, T2, T3, T4, T5, T6)>);
	}

	[Token(Token = "0x6000863")]
	public static UniTask<(T1, T2, T3, T4, T5, T6, T7)> WhenAll<T1, T2, T3, T4, T5, T6, T7>(this (UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7) tasks)
	{
		return default(UniTask<(T1, T2, T3, T4, T5, T6, T7)>);
	}
}
