using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using Il2CppDummyDll;
using Naninovel.Async;
using Naninovel.Async.CompilerServices;
using Naninovel.Async.Internal;

namespace Naninovel;

[Token(Token = "0x2000134")]
[AsyncMethodBuilder(typeof(AsyncUniTaskMethodBuilder))]
public readonly struct UniTask : IEquatable<UniTask>
{
	[Token(Token = "0x2000135")]
	private class AsyncUnitAwaiter
{
		[Token(Token = "0x40004E2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		private readonly IAwaiter awaiter;

		[Token(Token = "0x170000FD")]
		public bool IsCompleted
		{
			[Token(Token = "0x6000601")]
			[Address(RVA = "0x5153E0", Offset = "0x513DE0", VA = "0x1805153E0", Slot = "6")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x170000FE")]
		public AwaiterStatus Status
		{
			[Token(Token = "0x6000602")]
			[Address(RVA = "0x515430", Offset = "0x513E30", VA = "0x180515430", Slot = "5")]
			get
			{
				return default(AwaiterStatus);
			}
		}

		[Token(Token = "0x6000600")]
		[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
		public AsyncUnitAwaiter(IAwaiter awaiter)
		{
		}

		[Token(Token = "0x6000603")]
		[Address(RVA = "0x515480", Offset = "0x513E80", VA = "0x180515480", Slot = "4")]
		public AsyncUnit GetResult()
		{
			return default(AsyncUnit);
		}

		[Token(Token = "0x6000604")]
		[Address(RVA = "0x515510", Offset = "0x513F10", VA = "0x180515510", Slot = "9")]
		public void OnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x6000605")]
		[Address(RVA = "0x515570", Offset = "0x513F70", VA = "0x180515570", Slot = "8")]
		public void UnsafeOnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x6000606")]
		[Address(RVA = "0x5155D0", Offset = "0x513FD0", VA = "0x1805155D0", Slot = "7")]
		private void Naninovel_002EAsync_002EIAwaiter_002EGetResult()
		{
		}
	}

	[Token(Token = "0x2000136")]
	private class IsCanceledAwaiter
{
		[Token(Token = "0x40004E3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		private readonly IAwaiter awaiter;

		[Token(Token = "0x170000FF")]
		public bool IsCompleted
		{
			[Token(Token = "0x6000608")]
			[Address(RVA = "0x515620", Offset = "0x514020", VA = "0x180515620", Slot = "6")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x17000100")]
		public AwaiterStatus Status
		{
			[Token(Token = "0x6000609")]
			[Address(RVA = "0x515670", Offset = "0x514070", VA = "0x180515670", Slot = "5")]
			get
			{
				return default(AwaiterStatus);
			}
		}

		[Token(Token = "0x6000607")]
		[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
		public IsCanceledAwaiter(IAwaiter awaiter)
		{
		}

		[Token(Token = "0x600060A")]
		[Address(RVA = "0x5156C0", Offset = "0x5140C0", VA = "0x1805156C0", Slot = "4")]
		public bool GetResult()
		{
			return default(bool);
		}

		[Token(Token = "0x600060B")]
		[Address(RVA = "0x515740", Offset = "0x514140", VA = "0x180515740", Slot = "9")]
		public void OnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x600060C")]
		[Address(RVA = "0x5157A0", Offset = "0x5141A0", VA = "0x1805157A0", Slot = "8")]
		public void UnsafeOnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x600060D")]
		[Address(RVA = "0x515800", Offset = "0x514200", VA = "0x180515800", Slot = "7")]
		private void Naninovel_002EAsync_002EIAwaiter_002EGetResult()
		{
		}
	}

	[Token(Token = "0x2000137")]
	public readonly struct Awaiter
{
		[Token(Token = "0x40004E4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private readonly UniTask task;

		[Token(Token = "0x17000101")]
		[DebuggerHidden]
		public bool IsCompleted
		{
			[Token(Token = "0x600060F")]
			[Address(RVA = "0x515850", Offset = "0x514250", VA = "0x180515850", Slot = "5")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x17000102")]
		[DebuggerHidden]
		public AwaiterStatus Status
		{
			[Token(Token = "0x6000610")]
			[Address(RVA = "0x5158E0", Offset = "0x5142E0", VA = "0x1805158E0", Slot = "4")]
			get
			{
				return default(AwaiterStatus);
			}
		}

		[Token(Token = "0x600060E")]
		[Address(RVA = "0x3912B0", Offset = "0x38FCB0", VA = "0x1803912B0")]
		[DebuggerHidden]
		public Awaiter(UniTask task)
		{
		}

		[Token(Token = "0x6000611")]
		[Address(RVA = "0x515970", Offset = "0x514370", VA = "0x180515970", Slot = "6")]
		[DebuggerHidden]
		public void GetResult()
		{
		}

		[Token(Token = "0x6000612")]
		[Address(RVA = "0x515A00", Offset = "0x514400", VA = "0x180515A00", Slot = "8")]
		[DebuggerHidden]
		public void OnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x6000613")]
		[Address(RVA = "0x515A80", Offset = "0x514480", VA = "0x180515A80", Slot = "7")]
		[DebuggerHidden]
		public void UnsafeOnCompleted(Action continuation)
		{
		}
	}

	[Token(Token = "0x2000138")]
	private class DelayFramePromise : PlayerLoopReusablePromiseBase<int>
	{
		[Token(Token = "0x40004E5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private readonly int delayFrameCount;

		[Token(Token = "0x40004E6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x4C")]
		private int currentFrameCount;

		[Token(Token = "0x6000614")]
		[Address(RVA = "0x515B00", Offset = "0x514500", VA = "0x180515B00")]
		public DelayFramePromise(int delayFrameCount, PlayerLoopTiming timing, CancellationToken cancellationToken)
		{
		}

		[Token(Token = "0x6000615")]
		[Address(RVA = "0x515B80", Offset = "0x514580", VA = "0x180515B80", Slot = "16")]
		protected override void OnRunningStart()
		{
		}

		[Token(Token = "0x6000616")]
		[Address(RVA = "0x515B90", Offset = "0x514590", VA = "0x180515B90", Slot = "17")]
		public override bool MoveNext()
		{
			return default(bool);
		}
	}

	[Token(Token = "0x2000139")]
	private class DelayPromise : PlayerLoopReusablePromiseBase
	{
		[Token(Token = "0x40004E7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private readonly float delayFrameTimeSpan;

		[Token(Token = "0x40004E8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
		private float elapsed;

		[Token(Token = "0x6000617")]
		[Address(RVA = "0x515C70", Offset = "0x514670", VA = "0x180515C70")]
		public DelayPromise(TimeSpan delayFrameTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken)
		{
		}

		[Token(Token = "0x6000618")]
		[Address(RVA = "0x515D50", Offset = "0x514750", VA = "0x180515D50", Slot = "15")]
		protected override void OnRunningStart()
		{
		}

		[Token(Token = "0x6000619")]
		[Address(RVA = "0x515D60", Offset = "0x514760", VA = "0x180515D60", Slot = "16")]
		public override bool MoveNext()
		{
			return default(bool);
		}
	}

	[Token(Token = "0x200013A")]
	private class DelayIgnoreTimeScalePromise : PlayerLoopReusablePromiseBase
	{
		[Token(Token = "0x40004E9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private readonly float delayFrameTimeSpan;

		[Token(Token = "0x40004EA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
		private float elapsed;

		[Token(Token = "0x600061A")]
		[Address(RVA = "0x515E90", Offset = "0x514890", VA = "0x180515E90")]
		public DelayIgnoreTimeScalePromise(TimeSpan delayFrameTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken)
		{
		}

		[Token(Token = "0x600061B")]
		[Address(RVA = "0x515D50", Offset = "0x514750", VA = "0x180515D50", Slot = "15")]
		protected override void OnRunningStart()
		{
		}

		[Token(Token = "0x600061C")]
		[Address(RVA = "0x515F70", Offset = "0x514970", VA = "0x180515F70", Slot = "16")]
		public override bool MoveNext()
		{
			return default(bool);
		}
	}

	[Token(Token = "0x200013B")]
	private static class CanceledUniTaskCache<T>
	{
		[Token(Token = "0x40004EB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public static readonly UniTask<T> Task;

		[Token(Token = "0x600061D")]
		static CanceledUniTaskCache()
		{
		}
	}

	[Token(Token = "0x200013C")]
	private class WaitUntilPromise : PlayerLoopReusablePromiseBase
	{
		[Token(Token = "0x40004EC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private readonly Func<bool> predicate;

		[Token(Token = "0x600061E")]
		[Address(RVA = "0x5160A0", Offset = "0x514AA0", VA = "0x1805160A0")]
		public WaitUntilPromise(Func<bool> predicate, PlayerLoopTiming timing, CancellationToken cancellationToken)
		{
		}

		[Token(Token = "0x600061F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "15")]
		protected override void OnRunningStart()
		{
		}

		[Token(Token = "0x6000620")]
		[Address(RVA = "0x516160", Offset = "0x514B60", VA = "0x180516160", Slot = "16")]
		public override bool MoveNext()
		{
			return default(bool);
		}
	}

	[Token(Token = "0x200013D")]
	private class WaitWhilePromise : PlayerLoopReusablePromiseBase
	{
		[Token(Token = "0x40004ED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private readonly Func<bool> predicate;

		[Token(Token = "0x6000621")]
		[Address(RVA = "0x5160A0", Offset = "0x514AA0", VA = "0x1805160A0")]
		public WaitWhilePromise(Func<bool> predicate, PlayerLoopTiming timing, CancellationToken cancellationToken)
		{
		}

		[Token(Token = "0x6000622")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "15")]
		protected override void OnRunningStart()
		{
		}

		[Token(Token = "0x6000623")]
		[Address(RVA = "0x516260", Offset = "0x514C60", VA = "0x180516260", Slot = "16")]
		public override bool MoveNext()
		{
			return default(bool);
		}
	}

	[Token(Token = "0x200013E")]
	private class WaitUntilValueChangedUnityObjectPromise<T, U> : PlayerLoopReusablePromiseBase<U>
	{
		[Token(Token = "0x40004EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private readonly T target;

		[Token(Token = "0x40004EF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private readonly Func<T, U> monitorFunction;

		[Token(Token = "0x40004F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private readonly IEqualityComparer<U> equalityComparer;

		[Token(Token = "0x40004F1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private U currentValue;

		[Token(Token = "0x6000624")]
		public WaitUntilValueChangedUnityObjectPromise(T target, Func<T, U> monitorFunction, IEqualityComparer<U> equalityComparer, PlayerLoopTiming timing, CancellationToken cancellationToken)
		{
		}

		[Token(Token = "0x6000625")]
		protected override void OnRunningStart()
		{
		}

		[Token(Token = "0x6000626")]
		public override bool MoveNext()
		{
			return default(bool);
		}
	}

	[Token(Token = "0x200013F")]
	private class WaitUntilValueChangedStandardObjectPromise<T, U> : PlayerLoopReusablePromiseBase<U> where T : class
	{
		[Token(Token = "0x40004F2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private readonly WeakReference<T> target;

		[Token(Token = "0x40004F3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private readonly Func<T, U> monitorFunction;

		[Token(Token = "0x40004F4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private readonly IEqualityComparer<U> equalityComparer;

		[Token(Token = "0x40004F5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private U currentValue;

		[Token(Token = "0x6000627")]
		public WaitUntilValueChangedStandardObjectPromise(T target, Func<T, U> monitorFunction, IEqualityComparer<U> equalityComparer, PlayerLoopTiming timing, CancellationToken cancellationToken)
		{
		}

		[Token(Token = "0x6000628")]
		protected override void OnRunningStart()
		{
		}

		[Token(Token = "0x6000629")]
		public override bool MoveNext()
		{
			return default(bool);
		}
	}

	[Token(Token = "0x2000140")]
	private class WhenAllPromise<T>
	{
		[Token(Token = "0x2000141")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
{
			[Token(Token = "0x40004FA")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private readonly WhenAllPromise<T> parent;

			[Token(Token = "0x17000103")]
			public bool IsCompleted
			{
				[Token(Token = "0x600062F")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x600062E")]
			public Awaiter(WhenAllPromise<T> parent)
			{
			}

			[Token(Token = "0x6000630")]
			public T[] GetResult()
			{
				return null;
			}

			[Token(Token = "0x6000631")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x6000632")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000142")]
		[CompilerGenerated]
		private struct _003CRunTask_003Ed__6
{
			[Token(Token = "0x40004FB")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40004FC")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40004FD")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T> task;

			[Token(Token = "0x40004FE")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T> _003C_003E4__this;

			[Token(Token = "0x40004FF")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int index;

			[Token(Token = "0x4000500")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000633")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000634")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x40004F6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private readonly T[] result;

		[Token(Token = "0x40004F7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int completeCount;

		[Token(Token = "0x40004F8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Action whenComplete;

		[Token(Token = "0x40004F9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x600062A")]
		public WhenAllPromise(UniTask<T>[] tasks, int tasksLength)
		{
		}

		[Token(Token = "0x600062B")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x600062C")]
		[AsyncStateMachine(typeof(WhenAllPromise<>._003CRunTask_003Ed__6))]
		private UniTaskVoid RunTask(UniTask<T> task, int index)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x600062D")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[Token(Token = "0x2000143")]
	private class WhenAllPromise
	{
		[Token(Token = "0x2000144")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			[Token(Token = "0x4000505")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private readonly WhenAllPromise parent;

			[Token(Token = "0x17000104")]
			public bool IsCompleted
			{
				[Token(Token = "0x600063A")]
				[Address(RVA = "0x516840", Offset = "0x515240", VA = "0x180516840")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x6000639")]
			[Address(RVA = "0x3912B0", Offset = "0x38FCB0", VA = "0x1803912B0")]
			public Awaiter(WhenAllPromise parent)
			{
			}

			[Token(Token = "0x600063B")]
			[Address(RVA = "0x516870", Offset = "0x515270", VA = "0x180516870")]
			public void GetResult()
			{
			}

			[Token(Token = "0x600063C")]
			[Address(RVA = "0x5168B0", Offset = "0x5152B0", VA = "0x1805168B0", Slot = "5")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x600063D")]
			[Address(RVA = "0x5168C0", Offset = "0x5152C0", VA = "0x1805168C0", Slot = "4")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000145")]
		[CompilerGenerated]
		private struct _003CRunTask_003Ed__6
{
			[Token(Token = "0x4000506")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000507")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000508")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
			public UniTask task;

			[Token(Token = "0x4000509")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
			public WhenAllPromise _003C_003E4__this;

			[Token(Token = "0x400050A")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
			private UniTask.Awaiter _003C_003Eu__1;

			[Token(Token = "0x600063E")]
			[Address(RVA = "0x516970", Offset = "0x515370", VA = "0x180516970", Slot = "4")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x600063F")]
			[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x4000501")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		private int completeCount;

		[Token(Token = "0x4000502")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
		private readonly int resultLength;

		[Token(Token = "0x4000503")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		private Action whenComplete;

		[Token(Token = "0x4000504")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x6000635")]
		[Address(RVA = "0x516360", Offset = "0x514D60", VA = "0x180516360")]
		public WhenAllPromise(UniTask[] tasks, int tasksLength)
		{
		}

		[Token(Token = "0x6000636")]
		[Address(RVA = "0x5166A0", Offset = "0x5150A0", VA = "0x1805166A0")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x6000637")]
		[Address(RVA = "0x5166D0", Offset = "0x5150D0", VA = "0x1805166D0")]
		[AsyncStateMachine(typeof(_003CRunTask_003Ed__6))]
		private UniTaskVoid RunTask(UniTask task, int index)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000638")]
		[Address(RVA = "0x390E40", Offset = "0x38F840", VA = "0x180390E40")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[Token(Token = "0x2000146")]
	private class WhenAllPromise<T1, T2>
	{
		[Token(Token = "0x2000147")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			[Token(Token = "0x4000511")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private WhenAllPromise<T1, T2> parent;

			[Token(Token = "0x17000105")]
			public bool IsCompleted
			{
				[Token(Token = "0x6000648")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x6000647")]
			public Awaiter(WhenAllPromise<T1, T2> parent)
			{
			}

			[Token(Token = "0x6000649")]
			public (T1, T2) GetResult()
			{
				return default((T1, T2));
			}

			[Token(Token = "0x600064A")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x600064B")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000148")]
		[CompilerGenerated]
		private struct _003CRunTask1Async_003Ed__9
{
			[Token(Token = "0x4000512")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000513")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000514")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T1> task;

			[Token(Token = "0x4000515")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2> _003C_003E4__this;

			[Token(Token = "0x4000516")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T1>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x600064C")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x600064D")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000149")]
		[CompilerGenerated]
		private struct _003CRunTask2Async_003Ed__11
{
			[Token(Token = "0x4000517")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000518")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000519")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T2> task;

			[Token(Token = "0x400051A")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2> _003C_003E4__this;

			[Token(Token = "0x400051B")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T2>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x600064E")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x600064F")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x400050B")]
		private const int MaxCount = 2;

		[Token(Token = "0x400050C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T1 result1;

		[Token(Token = "0x400050D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T2 result2;

		[Token(Token = "0x400050E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x400050F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int completeCount;

		[Token(Token = "0x4000510")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Action whenComplete;

		[Token(Token = "0x6000640")]
		public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2)
		{
		}

		[Token(Token = "0x6000641")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x6000642")]
		private void RunTask1(UniTask<T1> task)
		{
		}

		[Token(Token = "0x6000643")]
		[AsyncStateMachine(typeof(WhenAllPromise<, >._003CRunTask1Async_003Ed__9))]
		private UniTaskVoid RunTask1Async(UniTask<T1> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000644")]
		private void RunTask2(UniTask<T2> task)
		{
		}

		[Token(Token = "0x6000645")]
		[AsyncStateMachine(typeof(WhenAllPromise<, >._003CRunTask2Async_003Ed__11))]
		private UniTaskVoid RunTask2Async(UniTask<T2> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000646")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[Token(Token = "0x200014A")]
	private class WhenAllPromise<T1, T2, T3>
	{
		[Token(Token = "0x200014B")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			[Token(Token = "0x4000523")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private WhenAllPromise<T1, T2, T3> parent;

			[Token(Token = "0x17000106")]
			public bool IsCompleted
			{
				[Token(Token = "0x600065A")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x6000659")]
			public Awaiter(WhenAllPromise<T1, T2, T3> parent)
			{
			}

			[Token(Token = "0x600065B")]
			public (T1, T2, T3) GetResult()
			{
				return default((T1, T2, T3));
			}

			[Token(Token = "0x600065C")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x600065D")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200014C")]
		[CompilerGenerated]
		private struct _003CRunTask1Async_003Ed__10
{
			[Token(Token = "0x4000524")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000525")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000526")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T1> task;

			[Token(Token = "0x4000527")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3> _003C_003E4__this;

			[Token(Token = "0x4000528")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T1>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x600065E")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x600065F")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200014D")]
		[CompilerGenerated]
		private struct _003CRunTask2Async_003Ed__12
{
			[Token(Token = "0x4000529")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x400052A")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x400052B")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T2> task;

			[Token(Token = "0x400052C")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3> _003C_003E4__this;

			[Token(Token = "0x400052D")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T2>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000660")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000661")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200014E")]
		[CompilerGenerated]
		private struct _003CRunTask3Async_003Ed__14
{
			[Token(Token = "0x400052E")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x400052F")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000530")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T3> task;

			[Token(Token = "0x4000531")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3> _003C_003E4__this;

			[Token(Token = "0x4000532")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T3>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000662")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000663")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x400051C")]
		private const int MaxCount = 3;

		[Token(Token = "0x400051D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T1 result1;

		[Token(Token = "0x400051E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T2 result2;

		[Token(Token = "0x400051F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T3 result3;

		[Token(Token = "0x4000520")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x4000521")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int completeCount;

		[Token(Token = "0x4000522")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Action whenComplete;

		[Token(Token = "0x6000650")]
		public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
		{
		}

		[Token(Token = "0x6000651")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x6000652")]
		private void RunTask1(UniTask<T1> task)
		{
		}

		[Token(Token = "0x6000653")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , >._003CRunTask1Async_003Ed__10))]
		private UniTaskVoid RunTask1Async(UniTask<T1> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000654")]
		private void RunTask2(UniTask<T2> task)
		{
		}

		[Token(Token = "0x6000655")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , >._003CRunTask2Async_003Ed__12))]
		private UniTaskVoid RunTask2Async(UniTask<T2> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000656")]
		private void RunTask3(UniTask<T3> task)
		{
		}

		[Token(Token = "0x6000657")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , >._003CRunTask3Async_003Ed__14))]
		private UniTaskVoid RunTask3Async(UniTask<T3> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000658")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[Token(Token = "0x200014F")]
	private class WhenAllPromise<T1, T2, T3, T4>
	{
		[Token(Token = "0x2000150")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			[Token(Token = "0x400053B")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private WhenAllPromise<T1, T2, T3, T4> parent;

			[Token(Token = "0x17000107")]
			public bool IsCompleted
			{
				[Token(Token = "0x6000670")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x600066F")]
			public Awaiter(WhenAllPromise<T1, T2, T3, T4> parent)
			{
			}

			[Token(Token = "0x6000671")]
			public (T1, T2, T3, T4) GetResult()
			{
				return default((T1, T2, T3, T4));
			}

			[Token(Token = "0x6000672")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x6000673")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000151")]
		[CompilerGenerated]
		private struct _003CRunTask1Async_003Ed__11
{
			[Token(Token = "0x400053C")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x400053D")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x400053E")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T1> task;

			[Token(Token = "0x400053F")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4> _003C_003E4__this;

			[Token(Token = "0x4000540")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T1>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000674")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000675")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000152")]
		[CompilerGenerated]
		private struct _003CRunTask2Async_003Ed__13
{
			[Token(Token = "0x4000541")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000542")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000543")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T2> task;

			[Token(Token = "0x4000544")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4> _003C_003E4__this;

			[Token(Token = "0x4000545")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T2>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000676")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000677")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000153")]
		[CompilerGenerated]
		private struct _003CRunTask3Async_003Ed__15
{
			[Token(Token = "0x4000546")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000547")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000548")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T3> task;

			[Token(Token = "0x4000549")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4> _003C_003E4__this;

			[Token(Token = "0x400054A")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T3>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000678")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000679")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000154")]
		[CompilerGenerated]
		private struct _003CRunTask4Async_003Ed__17
{
			[Token(Token = "0x400054B")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x400054C")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x400054D")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T4> task;

			[Token(Token = "0x400054E")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4> _003C_003E4__this;

			[Token(Token = "0x400054F")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T4>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x600067A")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x600067B")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x4000533")]
		private const int MaxCount = 4;

		[Token(Token = "0x4000534")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T1 result1;

		[Token(Token = "0x4000535")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T2 result2;

		[Token(Token = "0x4000536")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T3 result3;

		[Token(Token = "0x4000537")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T4 result4;

		[Token(Token = "0x4000538")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x4000539")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int completeCount;

		[Token(Token = "0x400053A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Action whenComplete;

		[Token(Token = "0x6000664")]
		public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
		{
		}

		[Token(Token = "0x6000665")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x6000666")]
		private void RunTask1(UniTask<T1> task)
		{
		}

		[Token(Token = "0x6000667")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , >._003CRunTask1Async_003Ed__11))]
		private UniTaskVoid RunTask1Async(UniTask<T1> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000668")]
		private void RunTask2(UniTask<T2> task)
		{
		}

		[Token(Token = "0x6000669")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , >._003CRunTask2Async_003Ed__13))]
		private UniTaskVoid RunTask2Async(UniTask<T2> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x600066A")]
		private void RunTask3(UniTask<T3> task)
		{
		}

		[Token(Token = "0x600066B")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , >._003CRunTask3Async_003Ed__15))]
		private UniTaskVoid RunTask3Async(UniTask<T3> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x600066C")]
		private void RunTask4(UniTask<T4> task)
		{
		}

		[Token(Token = "0x600066D")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , >._003CRunTask4Async_003Ed__17))]
		private UniTaskVoid RunTask4Async(UniTask<T4> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x600066E")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[Token(Token = "0x2000155")]
	private class WhenAllPromise<T1, T2, T3, T4, T5>
	{
		[Token(Token = "0x2000156")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			[Token(Token = "0x4000559")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private WhenAllPromise<T1, T2, T3, T4, T5> parent;

			[Token(Token = "0x17000108")]
			public bool IsCompleted
			{
				[Token(Token = "0x600068A")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x6000689")]
			public Awaiter(WhenAllPromise<T1, T2, T3, T4, T5> parent)
			{
			}

			[Token(Token = "0x600068B")]
			public (T1, T2, T3, T4, T5) GetResult()
			{
				return default((T1, T2, T3, T4, T5));
			}

			[Token(Token = "0x600068C")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x600068D")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000157")]
		[CompilerGenerated]
		private struct _003CRunTask1Async_003Ed__12
{
			[Token(Token = "0x400055A")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x400055B")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x400055C")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T1> task;

			[Token(Token = "0x400055D")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5> _003C_003E4__this;

			[Token(Token = "0x400055E")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T1>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x600068E")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x600068F")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000158")]
		[CompilerGenerated]
		private struct _003CRunTask2Async_003Ed__14
{
			[Token(Token = "0x400055F")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000560")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000561")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T2> task;

			[Token(Token = "0x4000562")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5> _003C_003E4__this;

			[Token(Token = "0x4000563")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T2>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000690")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000691")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000159")]
		[CompilerGenerated]
		private struct _003CRunTask3Async_003Ed__16
{
			[Token(Token = "0x4000564")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000565")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000566")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T3> task;

			[Token(Token = "0x4000567")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5> _003C_003E4__this;

			[Token(Token = "0x4000568")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T3>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000692")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000693")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200015A")]
		[CompilerGenerated]
		private struct _003CRunTask4Async_003Ed__18
{
			[Token(Token = "0x4000569")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x400056A")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x400056B")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T4> task;

			[Token(Token = "0x400056C")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5> _003C_003E4__this;

			[Token(Token = "0x400056D")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T4>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000694")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000695")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200015B")]
		[CompilerGenerated]
		private struct _003CRunTask5Async_003Ed__20
{
			[Token(Token = "0x400056E")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x400056F")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000570")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T5> task;

			[Token(Token = "0x4000571")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5> _003C_003E4__this;

			[Token(Token = "0x4000572")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T5>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000696")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000697")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x4000550")]
		private const int MaxCount = 5;

		[Token(Token = "0x4000551")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T1 result1;

		[Token(Token = "0x4000552")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T2 result2;

		[Token(Token = "0x4000553")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T3 result3;

		[Token(Token = "0x4000554")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T4 result4;

		[Token(Token = "0x4000555")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T5 result5;

		[Token(Token = "0x4000556")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x4000557")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int completeCount;

		[Token(Token = "0x4000558")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Action whenComplete;

		[Token(Token = "0x600067C")]
		public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
		{
		}

		[Token(Token = "0x600067D")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x600067E")]
		private void RunTask1(UniTask<T1> task)
		{
		}

		[Token(Token = "0x600067F")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , >._003CRunTask1Async_003Ed__12))]
		private UniTaskVoid RunTask1Async(UniTask<T1> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000680")]
		private void RunTask2(UniTask<T2> task)
		{
		}

		[Token(Token = "0x6000681")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , >._003CRunTask2Async_003Ed__14))]
		private UniTaskVoid RunTask2Async(UniTask<T2> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000682")]
		private void RunTask3(UniTask<T3> task)
		{
		}

		[Token(Token = "0x6000683")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , >._003CRunTask3Async_003Ed__16))]
		private UniTaskVoid RunTask3Async(UniTask<T3> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000684")]
		private void RunTask4(UniTask<T4> task)
		{
		}

		[Token(Token = "0x6000685")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , >._003CRunTask4Async_003Ed__18))]
		private UniTaskVoid RunTask4Async(UniTask<T4> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000686")]
		private void RunTask5(UniTask<T5> task)
		{
		}

		[Token(Token = "0x6000687")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , >._003CRunTask5Async_003Ed__20))]
		private UniTaskVoid RunTask5Async(UniTask<T5> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000688")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[Token(Token = "0x200015C")]
	private class WhenAllPromise<T1, T2, T3, T4, T5, T6>
	{
		[Token(Token = "0x200015D")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			[Token(Token = "0x400057D")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private WhenAllPromise<T1, T2, T3, T4, T5, T6> parent;

			[Token(Token = "0x17000109")]
			public bool IsCompleted
			{
				[Token(Token = "0x60006A8")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x60006A7")]
			public Awaiter(WhenAllPromise<T1, T2, T3, T4, T5, T6> parent)
			{
			}

			[Token(Token = "0x60006A9")]
			public (T1, T2, T3, T4, T5, T6) GetResult()
			{
				return default((T1, T2, T3, T4, T5, T6));
			}

			[Token(Token = "0x60006AA")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x60006AB")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200015E")]
		[CompilerGenerated]
		private struct _003CRunTask1Async_003Ed__13
{
			[Token(Token = "0x400057E")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x400057F")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000580")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T1> task;

			[Token(Token = "0x4000581")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5, T6> _003C_003E4__this;

			[Token(Token = "0x4000582")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T1>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60006AC")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60006AD")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200015F")]
		[CompilerGenerated]
		private struct _003CRunTask2Async_003Ed__15
{
			[Token(Token = "0x4000583")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000584")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000585")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T2> task;

			[Token(Token = "0x4000586")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5, T6> _003C_003E4__this;

			[Token(Token = "0x4000587")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T2>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60006AE")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60006AF")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000160")]
		[CompilerGenerated]
		private struct _003CRunTask3Async_003Ed__17
{
			[Token(Token = "0x4000588")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000589")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x400058A")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T3> task;

			[Token(Token = "0x400058B")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5, T6> _003C_003E4__this;

			[Token(Token = "0x400058C")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T3>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60006B0")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60006B1")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000161")]
		[CompilerGenerated]
		private struct _003CRunTask4Async_003Ed__19
{
			[Token(Token = "0x400058D")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x400058E")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x400058F")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T4> task;

			[Token(Token = "0x4000590")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5, T6> _003C_003E4__this;

			[Token(Token = "0x4000591")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T4>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60006B2")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60006B3")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000162")]
		[CompilerGenerated]
		private struct _003CRunTask5Async_003Ed__21
{
			[Token(Token = "0x4000592")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000593")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000594")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T5> task;

			[Token(Token = "0x4000595")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5, T6> _003C_003E4__this;

			[Token(Token = "0x4000596")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T5>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60006B4")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60006B5")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000163")]
		[CompilerGenerated]
		private struct _003CRunTask6Async_003Ed__23
{
			[Token(Token = "0x4000597")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000598")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000599")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T6> task;

			[Token(Token = "0x400059A")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5, T6> _003C_003E4__this;

			[Token(Token = "0x400059B")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T6>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60006B6")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60006B7")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x4000573")]
		private const int MaxCount = 6;

		[Token(Token = "0x4000574")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T1 result1;

		[Token(Token = "0x4000575")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T2 result2;

		[Token(Token = "0x4000576")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T3 result3;

		[Token(Token = "0x4000577")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T4 result4;

		[Token(Token = "0x4000578")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T5 result5;

		[Token(Token = "0x4000579")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T6 result6;

		[Token(Token = "0x400057A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x400057B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int completeCount;

		[Token(Token = "0x400057C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Action whenComplete;

		[Token(Token = "0x6000698")]
		public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
		{
		}

		[Token(Token = "0x6000699")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x600069A")]
		private void RunTask1(UniTask<T1> task)
		{
		}

		[Token(Token = "0x600069B")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , , >._003CRunTask1Async_003Ed__13))]
		private UniTaskVoid RunTask1Async(UniTask<T1> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x600069C")]
		private void RunTask2(UniTask<T2> task)
		{
		}

		[Token(Token = "0x600069D")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , , >._003CRunTask2Async_003Ed__15))]
		private UniTaskVoid RunTask2Async(UniTask<T2> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x600069E")]
		private void RunTask3(UniTask<T3> task)
		{
		}

		[Token(Token = "0x600069F")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , , >._003CRunTask3Async_003Ed__17))]
		private UniTaskVoid RunTask3Async(UniTask<T3> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x60006A0")]
		private void RunTask4(UniTask<T4> task)
		{
		}

		[Token(Token = "0x60006A1")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , , >._003CRunTask4Async_003Ed__19))]
		private UniTaskVoid RunTask4Async(UniTask<T4> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x60006A2")]
		private void RunTask5(UniTask<T5> task)
		{
		}

		[Token(Token = "0x60006A3")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , , >._003CRunTask5Async_003Ed__21))]
		private UniTaskVoid RunTask5Async(UniTask<T5> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x60006A4")]
		private void RunTask6(UniTask<T6> task)
		{
		}

		[Token(Token = "0x60006A5")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , , >._003CRunTask6Async_003Ed__23))]
		private UniTaskVoid RunTask6Async(UniTask<T6> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x60006A6")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[Token(Token = "0x2000164")]
	private class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>
	{
		[Token(Token = "0x2000165")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			[Token(Token = "0x40005A7")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> parent;

			[Token(Token = "0x1700010A")]
			public bool IsCompleted
			{
				[Token(Token = "0x60006CA")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x60006C9")]
			public Awaiter(WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> parent)
			{
			}

			[Token(Token = "0x60006CB")]
			public (T1, T2, T3, T4, T5, T6, T7) GetResult()
			{
				return default((T1, T2, T3, T4, T5, T6, T7));
			}

			[Token(Token = "0x60006CC")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x60006CD")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000166")]
		[CompilerGenerated]
		private struct _003CRunTask1Async_003Ed__14
{
			[Token(Token = "0x40005A8")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40005A9")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40005AA")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T1> task;

			[Token(Token = "0x40005AB")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> _003C_003E4__this;

			[Token(Token = "0x40005AC")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T1>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60006CE")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60006CF")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000167")]
		[CompilerGenerated]
		private struct _003CRunTask2Async_003Ed__16
{
			[Token(Token = "0x40005AD")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40005AE")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40005AF")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T2> task;

			[Token(Token = "0x40005B0")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> _003C_003E4__this;

			[Token(Token = "0x40005B1")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T2>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60006D0")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60006D1")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000168")]
		[CompilerGenerated]
		private struct _003CRunTask3Async_003Ed__18
{
			[Token(Token = "0x40005B2")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40005B3")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40005B4")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T3> task;

			[Token(Token = "0x40005B5")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> _003C_003E4__this;

			[Token(Token = "0x40005B6")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T3>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60006D2")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60006D3")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000169")]
		[CompilerGenerated]
		private struct _003CRunTask4Async_003Ed__20
{
			[Token(Token = "0x40005B7")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40005B8")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40005B9")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T4> task;

			[Token(Token = "0x40005BA")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> _003C_003E4__this;

			[Token(Token = "0x40005BB")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T4>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60006D4")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60006D5")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200016A")]
		[CompilerGenerated]
		private struct _003CRunTask5Async_003Ed__22
{
			[Token(Token = "0x40005BC")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40005BD")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40005BE")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T5> task;

			[Token(Token = "0x40005BF")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> _003C_003E4__this;

			[Token(Token = "0x40005C0")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T5>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60006D6")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60006D7")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200016B")]
		[CompilerGenerated]
		private struct _003CRunTask6Async_003Ed__24
{
			[Token(Token = "0x40005C1")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40005C2")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40005C3")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T6> task;

			[Token(Token = "0x40005C4")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> _003C_003E4__this;

			[Token(Token = "0x40005C5")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T6>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60006D8")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60006D9")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200016C")]
		[CompilerGenerated]
		private struct _003CRunTask7Async_003Ed__26
{
			[Token(Token = "0x40005C6")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40005C7")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40005C8")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T7> task;

			[Token(Token = "0x40005C9")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> _003C_003E4__this;

			[Token(Token = "0x40005CA")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T7>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60006DA")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60006DB")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x400059C")]
		private const int MaxCount = 7;

		[Token(Token = "0x400059D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T1 result1;

		[Token(Token = "0x400059E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T2 result2;

		[Token(Token = "0x400059F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T3 result3;

		[Token(Token = "0x40005A0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T4 result4;

		[Token(Token = "0x40005A1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T5 result5;

		[Token(Token = "0x40005A2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T6 result6;

		[Token(Token = "0x40005A3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T7 result7;

		[Token(Token = "0x40005A4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x40005A5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int completeCount;

		[Token(Token = "0x40005A6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Action whenComplete;

		[Token(Token = "0x60006B8")]
		public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
		{
		}

		[Token(Token = "0x60006B9")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x60006BA")]
		private void RunTask1(UniTask<T1> task)
		{
		}

		[Token(Token = "0x60006BB")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , , , >._003CRunTask1Async_003Ed__14))]
		private UniTaskVoid RunTask1Async(UniTask<T1> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x60006BC")]
		private void RunTask2(UniTask<T2> task)
		{
		}

		[Token(Token = "0x60006BD")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , , , >._003CRunTask2Async_003Ed__16))]
		private UniTaskVoid RunTask2Async(UniTask<T2> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x60006BE")]
		private void RunTask3(UniTask<T3> task)
		{
		}

		[Token(Token = "0x60006BF")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , , , >._003CRunTask3Async_003Ed__18))]
		private UniTaskVoid RunTask3Async(UniTask<T3> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x60006C0")]
		private void RunTask4(UniTask<T4> task)
		{
		}

		[Token(Token = "0x60006C1")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , , , >._003CRunTask4Async_003Ed__20))]
		private UniTaskVoid RunTask4Async(UniTask<T4> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x60006C2")]
		private void RunTask5(UniTask<T5> task)
		{
		}

		[Token(Token = "0x60006C3")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , , , >._003CRunTask5Async_003Ed__22))]
		private UniTaskVoid RunTask5Async(UniTask<T5> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x60006C4")]
		private void RunTask6(UniTask<T6> task)
		{
		}

		[Token(Token = "0x60006C5")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , , , >._003CRunTask6Async_003Ed__24))]
		private UniTaskVoid RunTask6Async(UniTask<T6> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x60006C6")]
		private void RunTask7(UniTask<T7> task)
		{
		}

		[Token(Token = "0x60006C7")]
		[AsyncStateMachine(typeof(WhenAllPromise<, , , , , , >._003CRunTask7Async_003Ed__26))]
		private UniTaskVoid RunTask7Async(UniTask<T7> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x60006C8")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[Token(Token = "0x200016D")]
	private class UnitWhenAnyPromise<T0>
	{
		[Token(Token = "0x200016E")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			[Token(Token = "0x40005D0")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private readonly UnitWhenAnyPromise<T0> parent;

			[Token(Token = "0x1700010C")]
			public bool IsCompleted
			{
				[Token(Token = "0x60006E3")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x60006E2")]
			public Awaiter(UnitWhenAnyPromise<T0> parent)
			{
			}

			[Token(Token = "0x60006E4")]
			public (bool, T0) GetResult()
			{
				return default((bool, T0));
			}

			[Token(Token = "0x60006E5")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x60006E6")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200016F")]
		[CompilerGenerated]
		private struct _003CRunTask0_003Ed__9
{
			[Token(Token = "0x40005D1")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40005D2")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40005D3")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T0> task;

			[Token(Token = "0x40005D4")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UnitWhenAnyPromise<T0> _003C_003E4__this;

			[Token(Token = "0x40005D5")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T0>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60006E7")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60006E8")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000170")]
		[CompilerGenerated]
		private struct _003CRunTask1_003Ed__10
{
			[Token(Token = "0x40005D6")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40005D7")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40005D8")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask task;

			[Token(Token = "0x40005D9")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UnitWhenAnyPromise<T0> _003C_003E4__this;

			[Token(Token = "0x40005DA")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60006E9")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60006EA")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x40005CB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T0 result0;

		[Token(Token = "0x40005CC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x40005CD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Action whenComplete;

		[Token(Token = "0x40005CE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int completeCount;

		[Token(Token = "0x40005CF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int winArgumentIndex;

		[Token(Token = "0x1700010B")]
		private bool IsCompleted
		{
			[Token(Token = "0x60006DC")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x60006DD")]
		public UnitWhenAnyPromise(UniTask<T0> task0, UniTask task1)
		{
		}

		[Token(Token = "0x60006DE")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x60006DF")]
		[AsyncStateMachine(typeof(UnitWhenAnyPromise<>._003CRunTask0_003Ed__9))]
		private UniTaskVoid RunTask0(UniTask<T0> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x60006E0")]
		[AsyncStateMachine(typeof(UnitWhenAnyPromise<>._003CRunTask1_003Ed__10))]
		private UniTaskVoid RunTask1(UniTask task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x60006E1")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[Token(Token = "0x2000171")]
	private class WhenAnyPromise<T>
	{
		[Token(Token = "0x2000172")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			[Token(Token = "0x40005E0")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private readonly WhenAnyPromise<T> parent;

			[Token(Token = "0x1700010E")]
			public bool IsCompleted
			{
				[Token(Token = "0x60006F1")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x60006F0")]
			public Awaiter(WhenAnyPromise<T> parent)
			{
			}

			[Token(Token = "0x60006F2")]
			public (int, T) GetResult()
			{
				return default((int, T));
			}

			[Token(Token = "0x60006F3")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x60006F4")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000173")]
		[CompilerGenerated]
		private struct _003CRunTask_003Ed__8
{
			[Token(Token = "0x40005E1")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40005E2")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40005E3")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T> task;

			[Token(Token = "0x40005E4")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T> _003C_003E4__this;

			[Token(Token = "0x40005E5")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int index;

			[Token(Token = "0x40005E6")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60006F5")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60006F6")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x40005DB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T result;

		[Token(Token = "0x40005DC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int completeCount;

		[Token(Token = "0x40005DD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int winArgumentIndex;

		[Token(Token = "0x40005DE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Action whenComplete;

		[Token(Token = "0x40005DF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x1700010D")]
		public bool IsComplete
		{
			[Token(Token = "0x60006EB")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x60006EC")]
		public WhenAnyPromise(UniTask<T>[] tasks)
		{
		}

		[Token(Token = "0x60006ED")]
		[AsyncStateMachine(typeof(WhenAnyPromise<>._003CRunTask_003Ed__8))]
		private UniTaskVoid RunTask(UniTask<T> task, int index)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x60006EE")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x60006EF")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[Token(Token = "0x2000174")]
	private class WhenAnyPromise
	{
		[Token(Token = "0x2000175")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			[Token(Token = "0x40005EB")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private readonly WhenAnyPromise parent;

			[Token(Token = "0x17000110")]
			public bool IsCompleted
			{
				[Token(Token = "0x60006FD")]
				[Address(RVA = "0x5171B0", Offset = "0x515BB0", VA = "0x1805171B0")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x60006FC")]
			[Address(RVA = "0x3912B0", Offset = "0x38FCB0", VA = "0x1803912B0")]
			public Awaiter(WhenAnyPromise parent)
			{
			}

			[Token(Token = "0x60006FE")]
			[Address(RVA = "0x5171F0", Offset = "0x515BF0", VA = "0x1805171F0")]
			public int GetResult()
			{
				return default(int);
			}

			[Token(Token = "0x60006FF")]
			[Address(RVA = "0x517240", Offset = "0x515C40", VA = "0x180517240", Slot = "5")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x6000700")]
			[Address(RVA = "0x517250", Offset = "0x515C50", VA = "0x180517250", Slot = "4")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000176")]
		[CompilerGenerated]
		private struct _003CRunTask_003Ed__7
{
			[Token(Token = "0x40005EC")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40005ED")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40005EE")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
			public UniTask task;

			[Token(Token = "0x40005EF")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
			public WhenAnyPromise _003C_003E4__this;

			[Token(Token = "0x40005F0")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
			public int index;

			[Token(Token = "0x40005F1")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
			private UniTask.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000701")]
			[Address(RVA = "0x517310", Offset = "0x515D10", VA = "0x180517310", Slot = "4")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000702")]
			[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x40005E7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		private int completeCount;

		[Token(Token = "0x40005E8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
		private int winArgumentIndex;

		[Token(Token = "0x40005E9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		private Action whenComplete;

		[Token(Token = "0x40005EA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x1700010F")]
		public bool IsComplete
		{
			[Token(Token = "0x60006F7")]
			[Address(RVA = "0x516D80", Offset = "0x515780", VA = "0x180516D80")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x60006F8")]
		[Address(RVA = "0x516DA0", Offset = "0x5157A0", VA = "0x180516DA0")]
		public WhenAnyPromise(UniTask[] tasks)
		{
		}

		[Token(Token = "0x60006F9")]
		[Address(RVA = "0x517030", Offset = "0x515A30", VA = "0x180517030")]
		[AsyncStateMachine(typeof(_003CRunTask_003Ed__7))]
		private UniTaskVoid RunTask(UniTask task, int index)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x60006FA")]
		[Address(RVA = "0x5166A0", Offset = "0x5150A0", VA = "0x1805166A0")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x60006FB")]
		[Address(RVA = "0x390E40", Offset = "0x38F840", VA = "0x180390E40")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[Token(Token = "0x2000177")]
	private class WhenAnyPromise<T0, T1>
	{
		[Token(Token = "0x2000178")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			[Token(Token = "0x40005F8")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private WhenAnyPromise<T0, T1> parent;

			[Token(Token = "0x17000112")]
			public bool IsCompleted
			{
				[Token(Token = "0x600070A")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x6000709")]
			public Awaiter(WhenAnyPromise<T0, T1> parent)
			{
			}

			[Token(Token = "0x600070B")]
			public (int, (bool, T0), (bool, T1)) GetResult()
			{
				return default((int, (bool, T0), (bool, T1)));
			}

			[Token(Token = "0x600070C")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x600070D")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000179")]
		[CompilerGenerated]
		private struct _003CRunTask0_003Ed__10
{
			[Token(Token = "0x40005F9")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40005FA")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40005FB")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T0> task;

			[Token(Token = "0x40005FC")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1> _003C_003E4__this;

			[Token(Token = "0x40005FD")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T0>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x600070E")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x600070F")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200017A")]
		[CompilerGenerated]
		private struct _003CRunTask1_003Ed__11
{
			[Token(Token = "0x40005FE")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40005FF")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000600")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T1> task;

			[Token(Token = "0x4000601")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1> _003C_003E4__this;

			[Token(Token = "0x4000602")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T1>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000710")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000711")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x40005F2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T0 result0;

		[Token(Token = "0x40005F3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T1 result1;

		[Token(Token = "0x40005F4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x40005F5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Action whenComplete;

		[Token(Token = "0x40005F6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int completeCount;

		[Token(Token = "0x40005F7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int winArgumentIndex;

		[Token(Token = "0x17000111")]
		private bool IsCompleted
		{
			[Token(Token = "0x6000703")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x6000704")]
		public WhenAnyPromise(UniTask<T0> task0, UniTask<T1> task1)
		{
		}

		[Token(Token = "0x6000705")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x6000706")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, >._003CRunTask0_003Ed__10))]
		private UniTaskVoid RunTask0(UniTask<T0> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000707")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, >._003CRunTask1_003Ed__11))]
		private UniTaskVoid RunTask1(UniTask<T1> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000708")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[Token(Token = "0x200017B")]
	private class WhenAnyPromise<T0, T1, T2>
	{
		[Token(Token = "0x200017C")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			[Token(Token = "0x400060A")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private WhenAnyPromise<T0, T1, T2> parent;

			[Token(Token = "0x17000114")]
			public bool IsCompleted
			{
				[Token(Token = "0x600071A")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x6000719")]
			public Awaiter(WhenAnyPromise<T0, T1, T2> parent)
			{
			}

			[Token(Token = "0x600071B")]
			public (int, (bool, T0), (bool, T1), (bool, T2)) GetResult()
			{
				return default((int, (bool, T0), (bool, T1), (bool, T2)));
			}

			[Token(Token = "0x600071C")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x600071D")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200017D")]
		[CompilerGenerated]
		private struct _003CRunTask0_003Ed__11
{
			[Token(Token = "0x400060B")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x400060C")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x400060D")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T0> task;

			[Token(Token = "0x400060E")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2> _003C_003E4__this;

			[Token(Token = "0x400060F")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T0>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x600071E")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x600071F")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200017E")]
		[CompilerGenerated]
		private struct _003CRunTask1_003Ed__12
{
			[Token(Token = "0x4000610")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000611")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000612")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T1> task;

			[Token(Token = "0x4000613")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2> _003C_003E4__this;

			[Token(Token = "0x4000614")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T1>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000720")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000721")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200017F")]
		[CompilerGenerated]
		private struct _003CRunTask2_003Ed__13
{
			[Token(Token = "0x4000615")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000616")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000617")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T2> task;

			[Token(Token = "0x4000618")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2> _003C_003E4__this;

			[Token(Token = "0x4000619")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T2>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000722")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000723")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x4000603")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T0 result0;

		[Token(Token = "0x4000604")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T1 result1;

		[Token(Token = "0x4000605")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T2 result2;

		[Token(Token = "0x4000606")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x4000607")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Action whenComplete;

		[Token(Token = "0x4000608")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int completeCount;

		[Token(Token = "0x4000609")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int winArgumentIndex;

		[Token(Token = "0x17000113")]
		private bool IsCompleted
		{
			[Token(Token = "0x6000712")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x6000713")]
		public WhenAnyPromise(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2)
		{
		}

		[Token(Token = "0x6000714")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x6000715")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , >._003CRunTask0_003Ed__11))]
		private UniTaskVoid RunTask0(UniTask<T0> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000716")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , >._003CRunTask1_003Ed__12))]
		private UniTaskVoid RunTask1(UniTask<T1> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000717")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , >._003CRunTask2_003Ed__13))]
		private UniTaskVoid RunTask2(UniTask<T2> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000718")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[Token(Token = "0x2000180")]
	private class WhenAnyPromise<T0, T1, T2, T3>
	{
		[Token(Token = "0x2000181")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			[Token(Token = "0x4000622")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private WhenAnyPromise<T0, T1, T2, T3> parent;

			[Token(Token = "0x17000116")]
			public bool IsCompleted
			{
				[Token(Token = "0x600072D")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x600072C")]
			public Awaiter(WhenAnyPromise<T0, T1, T2, T3> parent)
			{
			}

			[Token(Token = "0x600072E")]
			public (int, (bool, T0), (bool, T1), (bool, T2), (bool, T3)) GetResult()
			{
				return default((int, (bool, T0), (bool, T1), (bool, T2), (bool, T3)));
			}

			[Token(Token = "0x600072F")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x6000730")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000182")]
		[CompilerGenerated]
		private struct _003CRunTask0_003Ed__12
{
			[Token(Token = "0x4000623")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000624")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000625")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T0> task;

			[Token(Token = "0x4000626")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3> _003C_003E4__this;

			[Token(Token = "0x4000627")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T0>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000731")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000732")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000183")]
		[CompilerGenerated]
		private struct _003CRunTask1_003Ed__13
{
			[Token(Token = "0x4000628")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000629")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x400062A")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T1> task;

			[Token(Token = "0x400062B")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3> _003C_003E4__this;

			[Token(Token = "0x400062C")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T1>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000733")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000734")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000184")]
		[CompilerGenerated]
		private struct _003CRunTask2_003Ed__14
{
			[Token(Token = "0x400062D")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x400062E")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x400062F")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T2> task;

			[Token(Token = "0x4000630")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3> _003C_003E4__this;

			[Token(Token = "0x4000631")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T2>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000735")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000736")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000185")]
		[CompilerGenerated]
		private struct _003CRunTask3_003Ed__15
{
			[Token(Token = "0x4000632")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000633")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000634")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T3> task;

			[Token(Token = "0x4000635")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3> _003C_003E4__this;

			[Token(Token = "0x4000636")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T3>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000737")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000738")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x400061A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T0 result0;

		[Token(Token = "0x400061B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T1 result1;

		[Token(Token = "0x400061C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T2 result2;

		[Token(Token = "0x400061D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T3 result3;

		[Token(Token = "0x400061E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x400061F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Action whenComplete;

		[Token(Token = "0x4000620")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int completeCount;

		[Token(Token = "0x4000621")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int winArgumentIndex;

		[Token(Token = "0x17000115")]
		private bool IsCompleted
		{
			[Token(Token = "0x6000724")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x6000725")]
		public WhenAnyPromise(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
		{
		}

		[Token(Token = "0x6000726")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x6000727")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , >._003CRunTask0_003Ed__12))]
		private UniTaskVoid RunTask0(UniTask<T0> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000728")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , >._003CRunTask1_003Ed__13))]
		private UniTaskVoid RunTask1(UniTask<T1> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000729")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , >._003CRunTask2_003Ed__14))]
		private UniTaskVoid RunTask2(UniTask<T2> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x600072A")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , >._003CRunTask3_003Ed__15))]
		private UniTaskVoid RunTask3(UniTask<T3> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x600072B")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[Token(Token = "0x2000186")]
	private class WhenAnyPromise<T0, T1, T2, T3, T4>
	{
		[Token(Token = "0x2000187")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			[Token(Token = "0x4000640")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private WhenAnyPromise<T0, T1, T2, T3, T4> parent;

			[Token(Token = "0x17000118")]
			public bool IsCompleted
			{
				[Token(Token = "0x6000743")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x6000742")]
			public Awaiter(WhenAnyPromise<T0, T1, T2, T3, T4> parent)
			{
			}

			[Token(Token = "0x6000744")]
			public (int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4)) GetResult()
			{
				return default((int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4)));
			}

			[Token(Token = "0x6000745")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x6000746")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000188")]
		[CompilerGenerated]
		private struct _003CRunTask0_003Ed__13
{
			[Token(Token = "0x4000641")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000642")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000643")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T0> task;

			[Token(Token = "0x4000644")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4> _003C_003E4__this;

			[Token(Token = "0x4000645")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T0>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000747")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000748")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000189")]
		[CompilerGenerated]
		private struct _003CRunTask1_003Ed__14
{
			[Token(Token = "0x4000646")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000647")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000648")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T1> task;

			[Token(Token = "0x4000649")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4> _003C_003E4__this;

			[Token(Token = "0x400064A")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T1>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000749")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x600074A")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200018A")]
		[CompilerGenerated]
		private struct _003CRunTask2_003Ed__15
{
			[Token(Token = "0x400064B")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x400064C")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x400064D")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T2> task;

			[Token(Token = "0x400064E")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4> _003C_003E4__this;

			[Token(Token = "0x400064F")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T2>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x600074B")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x600074C")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200018B")]
		[CompilerGenerated]
		private struct _003CRunTask3_003Ed__16
{
			[Token(Token = "0x4000650")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000651")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000652")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T3> task;

			[Token(Token = "0x4000653")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4> _003C_003E4__this;

			[Token(Token = "0x4000654")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T3>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x600074D")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x600074E")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200018C")]
		[CompilerGenerated]
		private struct _003CRunTask4_003Ed__17
{
			[Token(Token = "0x4000655")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000656")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000657")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T4> task;

			[Token(Token = "0x4000658")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4> _003C_003E4__this;

			[Token(Token = "0x4000659")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T4>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x600074F")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000750")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x4000637")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T0 result0;

		[Token(Token = "0x4000638")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T1 result1;

		[Token(Token = "0x4000639")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T2 result2;

		[Token(Token = "0x400063A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T3 result3;

		[Token(Token = "0x400063B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T4 result4;

		[Token(Token = "0x400063C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x400063D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Action whenComplete;

		[Token(Token = "0x400063E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int completeCount;

		[Token(Token = "0x400063F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int winArgumentIndex;

		[Token(Token = "0x17000117")]
		private bool IsCompleted
		{
			[Token(Token = "0x6000739")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x600073A")]
		public WhenAnyPromise(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
		{
		}

		[Token(Token = "0x600073B")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x600073C")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , >._003CRunTask0_003Ed__13))]
		private UniTaskVoid RunTask0(UniTask<T0> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x600073D")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , >._003CRunTask1_003Ed__14))]
		private UniTaskVoid RunTask1(UniTask<T1> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x600073E")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , >._003CRunTask2_003Ed__15))]
		private UniTaskVoid RunTask2(UniTask<T2> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x600073F")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , >._003CRunTask3_003Ed__16))]
		private UniTaskVoid RunTask3(UniTask<T3> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000740")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , >._003CRunTask4_003Ed__17))]
		private UniTaskVoid RunTask4(UniTask<T4> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000741")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[Token(Token = "0x200018D")]
	private class WhenAnyPromise<T0, T1, T2, T3, T4, T5>
	{
		[Token(Token = "0x200018E")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			[Token(Token = "0x4000664")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private WhenAnyPromise<T0, T1, T2, T3, T4, T5> parent;

			[Token(Token = "0x1700011A")]
			public bool IsCompleted
			{
				[Token(Token = "0x600075C")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x600075B")]
			public Awaiter(WhenAnyPromise<T0, T1, T2, T3, T4, T5> parent)
			{
			}

			[Token(Token = "0x600075D")]
			public (int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4), (bool, T5)) GetResult()
			{
				return default((int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4), (bool, T5)));
			}

			[Token(Token = "0x600075E")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x600075F")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200018F")]
		[CompilerGenerated]
		private struct _003CRunTask0_003Ed__14
{
			[Token(Token = "0x4000665")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000666")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000667")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T0> task;

			[Token(Token = "0x4000668")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5> _003C_003E4__this;

			[Token(Token = "0x4000669")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T0>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000760")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000761")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000190")]
		[CompilerGenerated]
		private struct _003CRunTask1_003Ed__15
{
			[Token(Token = "0x400066A")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x400066B")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x400066C")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T1> task;

			[Token(Token = "0x400066D")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5> _003C_003E4__this;

			[Token(Token = "0x400066E")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T1>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000762")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000763")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000191")]
		[CompilerGenerated]
		private struct _003CRunTask2_003Ed__16
{
			[Token(Token = "0x400066F")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000670")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000671")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T2> task;

			[Token(Token = "0x4000672")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5> _003C_003E4__this;

			[Token(Token = "0x4000673")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T2>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000764")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000765")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000192")]
		[CompilerGenerated]
		private struct _003CRunTask3_003Ed__17
{
			[Token(Token = "0x4000674")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000675")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000676")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T3> task;

			[Token(Token = "0x4000677")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5> _003C_003E4__this;

			[Token(Token = "0x4000678")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T3>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000766")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000767")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000193")]
		[CompilerGenerated]
		private struct _003CRunTask4_003Ed__18
{
			[Token(Token = "0x4000679")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x400067A")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x400067B")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T4> task;

			[Token(Token = "0x400067C")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5> _003C_003E4__this;

			[Token(Token = "0x400067D")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T4>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000768")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000769")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000194")]
		[CompilerGenerated]
		private struct _003CRunTask5_003Ed__19
{
			[Token(Token = "0x400067E")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x400067F")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000680")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T5> task;

			[Token(Token = "0x4000681")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5> _003C_003E4__this;

			[Token(Token = "0x4000682")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T5>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x600076A")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x600076B")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x400065A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T0 result0;

		[Token(Token = "0x400065B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T1 result1;

		[Token(Token = "0x400065C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T2 result2;

		[Token(Token = "0x400065D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T3 result3;

		[Token(Token = "0x400065E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T4 result4;

		[Token(Token = "0x400065F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T5 result5;

		[Token(Token = "0x4000660")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x4000661")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Action whenComplete;

		[Token(Token = "0x4000662")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int completeCount;

		[Token(Token = "0x4000663")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int winArgumentIndex;

		[Token(Token = "0x17000119")]
		private bool IsCompleted
		{
			[Token(Token = "0x6000751")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x6000752")]
		public WhenAnyPromise(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
		{
		}

		[Token(Token = "0x6000753")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x6000754")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , >._003CRunTask0_003Ed__14))]
		private UniTaskVoid RunTask0(UniTask<T0> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000755")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , >._003CRunTask1_003Ed__15))]
		private UniTaskVoid RunTask1(UniTask<T1> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000756")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , >._003CRunTask2_003Ed__16))]
		private UniTaskVoid RunTask2(UniTask<T2> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000757")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , >._003CRunTask3_003Ed__17))]
		private UniTaskVoid RunTask3(UniTask<T3> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000758")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , >._003CRunTask4_003Ed__18))]
		private UniTaskVoid RunTask4(UniTask<T4> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000759")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , >._003CRunTask5_003Ed__19))]
		private UniTaskVoid RunTask5(UniTask<T5> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x600075A")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[Token(Token = "0x2000195")]
	private class WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6>
	{
		[Token(Token = "0x2000196")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			[Token(Token = "0x400068E")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6> parent;

			[Token(Token = "0x1700011C")]
			public bool IsCompleted
			{
				[Token(Token = "0x6000778")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x6000777")]
			public Awaiter(WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6> parent)
			{
			}

			[Token(Token = "0x6000779")]
			public (int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4), (bool, T5), (bool, T6)) GetResult()
			{
				return default((int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4), (bool, T5), (bool, T6)));
			}

			[Token(Token = "0x600077A")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x600077B")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000197")]
		[CompilerGenerated]
		private struct _003CRunTask0_003Ed__15
{
			[Token(Token = "0x400068F")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000690")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000691")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T0> task;

			[Token(Token = "0x4000692")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6> _003C_003E4__this;

			[Token(Token = "0x4000693")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T0>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x600077C")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x600077D")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000198")]
		[CompilerGenerated]
		private struct _003CRunTask1_003Ed__16
{
			[Token(Token = "0x4000694")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000695")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x4000696")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T1> task;

			[Token(Token = "0x4000697")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6> _003C_003E4__this;

			[Token(Token = "0x4000698")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T1>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x600077E")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x600077F")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x2000199")]
		[CompilerGenerated]
		private struct _003CRunTask2_003Ed__17
{
			[Token(Token = "0x4000699")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x400069A")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x400069B")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T2> task;

			[Token(Token = "0x400069C")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6> _003C_003E4__this;

			[Token(Token = "0x400069D")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T2>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000780")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000781")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200019A")]
		[CompilerGenerated]
		private struct _003CRunTask3_003Ed__18
{
			[Token(Token = "0x400069E")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x400069F")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40006A0")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T3> task;

			[Token(Token = "0x40006A1")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6> _003C_003E4__this;

			[Token(Token = "0x40006A2")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T3>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000782")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000783")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200019B")]
		[CompilerGenerated]
		private struct _003CRunTask4_003Ed__19
{
			[Token(Token = "0x40006A3")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40006A4")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40006A5")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T4> task;

			[Token(Token = "0x40006A6")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6> _003C_003E4__this;

			[Token(Token = "0x40006A7")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T4>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000784")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000785")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200019C")]
		[CompilerGenerated]
		private struct _003CRunTask5_003Ed__20
{
			[Token(Token = "0x40006A8")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40006A9")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40006AA")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T5> task;

			[Token(Token = "0x40006AB")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6> _003C_003E4__this;

			[Token(Token = "0x40006AC")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T5>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000786")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000787")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x200019D")]
		[CompilerGenerated]
		private struct _003CRunTask6_003Ed__21
{
			[Token(Token = "0x40006AD")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40006AE")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40006AF")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T6> task;

			[Token(Token = "0x40006B0")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6> _003C_003E4__this;

			[Token(Token = "0x40006B1")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T6>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000788")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000789")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x4000683")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T0 result0;

		[Token(Token = "0x4000684")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T1 result1;

		[Token(Token = "0x4000685")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T2 result2;

		[Token(Token = "0x4000686")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T3 result3;

		[Token(Token = "0x4000687")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T4 result4;

		[Token(Token = "0x4000688")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T5 result5;

		[Token(Token = "0x4000689")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T6 result6;

		[Token(Token = "0x400068A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x400068B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Action whenComplete;

		[Token(Token = "0x400068C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int completeCount;

		[Token(Token = "0x400068D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int winArgumentIndex;

		[Token(Token = "0x1700011B")]
		private bool IsCompleted
		{
			[Token(Token = "0x600076C")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x600076D")]
		public WhenAnyPromise(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
		{
		}

		[Token(Token = "0x600076E")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x600076F")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , , >._003CRunTask0_003Ed__15))]
		private UniTaskVoid RunTask0(UniTask<T0> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000770")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , , >._003CRunTask1_003Ed__16))]
		private UniTaskVoid RunTask1(UniTask<T1> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000771")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , , >._003CRunTask2_003Ed__17))]
		private UniTaskVoid RunTask2(UniTask<T2> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000772")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , , >._003CRunTask3_003Ed__18))]
		private UniTaskVoid RunTask3(UniTask<T3> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000773")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , , >._003CRunTask4_003Ed__19))]
		private UniTaskVoid RunTask4(UniTask<T4> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000774")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , , >._003CRunTask5_003Ed__20))]
		private UniTaskVoid RunTask5(UniTask<T5> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000775")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , , >._003CRunTask6_003Ed__21))]
		private UniTaskVoid RunTask6(UniTask<T6> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000776")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[Token(Token = "0x200019E")]
	private class WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6, T7>
	{
		[Token(Token = "0x200019F")]
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			[Token(Token = "0x40006BE")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6, T7> parent;

			[Token(Token = "0x1700011E")]
			public bool IsCompleted
			{
				[Token(Token = "0x6000797")]
				get
				{
					return default(bool);
				}
			}

			[Token(Token = "0x6000796")]
			public Awaiter(WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6, T7> parent)
			{
			}

			[Token(Token = "0x6000798")]
			public (int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4), (bool, T5), (bool, T6), (bool, T7)) GetResult()
			{
				return default((int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4), (bool, T5), (bool, T6), (bool, T7)));
			}

			[Token(Token = "0x6000799")]
			public void OnCompleted(Action continuation)
			{
			}

			[Token(Token = "0x600079A")]
			public void UnsafeOnCompleted(Action continuation)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x20001A0")]
		[CompilerGenerated]
		private struct _003CRunTask0_003Ed__16
{
			[Token(Token = "0x40006BF")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40006C0")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40006C1")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T0> task;

			[Token(Token = "0x40006C2")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6, T7> _003C_003E4__this;

			[Token(Token = "0x40006C3")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T0>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x600079B")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x600079C")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x20001A1")]
		[CompilerGenerated]
		private struct _003CRunTask1_003Ed__17
{
			[Token(Token = "0x40006C4")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40006C5")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40006C6")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T1> task;

			[Token(Token = "0x40006C7")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6, T7> _003C_003E4__this;

			[Token(Token = "0x40006C8")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T1>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x600079D")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x600079E")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x20001A2")]
		[CompilerGenerated]
		private struct _003CRunTask2_003Ed__18
{
			[Token(Token = "0x40006C9")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40006CA")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40006CB")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T2> task;

			[Token(Token = "0x40006CC")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6, T7> _003C_003E4__this;

			[Token(Token = "0x40006CD")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T2>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x600079F")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60007A0")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x20001A3")]
		[CompilerGenerated]
		private struct _003CRunTask3_003Ed__19
{
			[Token(Token = "0x40006CE")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40006CF")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40006D0")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T3> task;

			[Token(Token = "0x40006D1")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6, T7> _003C_003E4__this;

			[Token(Token = "0x40006D2")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T3>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60007A1")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60007A2")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x20001A4")]
		[CompilerGenerated]
		private struct _003CRunTask4_003Ed__20
{
			[Token(Token = "0x40006D3")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40006D4")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40006D5")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T4> task;

			[Token(Token = "0x40006D6")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6, T7> _003C_003E4__this;

			[Token(Token = "0x40006D7")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T4>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60007A3")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60007A4")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x20001A5")]
		[CompilerGenerated]
		private struct _003CRunTask5_003Ed__21
{
			[Token(Token = "0x40006D8")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40006D9")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40006DA")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T5> task;

			[Token(Token = "0x40006DB")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6, T7> _003C_003E4__this;

			[Token(Token = "0x40006DC")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T5>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60007A5")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60007A6")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x20001A6")]
		[CompilerGenerated]
		private struct _003CRunTask6_003Ed__22
{
			[Token(Token = "0x40006DD")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40006DE")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40006DF")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T6> task;

			[Token(Token = "0x40006E0")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6, T7> _003C_003E4__this;

			[Token(Token = "0x40006E1")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T6>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60007A7")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60007A8")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[StructLayout(3)]
		[Token(Token = "0x20001A7")]
		[CompilerGenerated]
		private struct _003CRunTask7_003Ed__23
{
			[Token(Token = "0x40006E2")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x40006E3")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			[Token(Token = "0x40006E4")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public UniTask<T7> task;

			[Token(Token = "0x40006E5")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6, T7> _003C_003E4__this;

			[Token(Token = "0x40006E6")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<T7>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x60007A9")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x60007AA")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x40006B2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T0 result0;

		[Token(Token = "0x40006B3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T1 result1;

		[Token(Token = "0x40006B4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T2 result2;

		[Token(Token = "0x40006B5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T3 result3;

		[Token(Token = "0x40006B6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T4 result4;

		[Token(Token = "0x40006B7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T5 result5;

		[Token(Token = "0x40006B8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T6 result6;

		[Token(Token = "0x40006B9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T7 result7;

		[Token(Token = "0x40006BA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x40006BB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Action whenComplete;

		[Token(Token = "0x40006BC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int completeCount;

		[Token(Token = "0x40006BD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int winArgumentIndex;

		[Token(Token = "0x1700011D")]
		private bool IsCompleted
		{
			[Token(Token = "0x600078A")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x600078B")]
		public WhenAnyPromise(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
		{
		}

		[Token(Token = "0x600078C")]
		private void TryCallContinuation()
		{
		}

		[Token(Token = "0x600078D")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , , , >._003CRunTask0_003Ed__16))]
		private UniTaskVoid RunTask0(UniTask<T0> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x600078E")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , , , >._003CRunTask1_003Ed__17))]
		private UniTaskVoid RunTask1(UniTask<T1> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x600078F")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , , , >._003CRunTask2_003Ed__18))]
		private UniTaskVoid RunTask2(UniTask<T2> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000790")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , , , >._003CRunTask3_003Ed__19))]
		private UniTaskVoid RunTask3(UniTask<T3> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000791")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , , , >._003CRunTask4_003Ed__20))]
		private UniTaskVoid RunTask4(UniTask<T4> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000792")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , , , >._003CRunTask5_003Ed__21))]
		private UniTaskVoid RunTask5(UniTask<T5> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000793")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , , , >._003CRunTask6_003Ed__22))]
		private UniTaskVoid RunTask6(UniTask<T6> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000794")]
		[AsyncStateMachine(typeof(WhenAnyPromise<, , , , , , , >._003CRunTask7_003Ed__23))]
		private UniTaskVoid RunTask7(UniTask<T7> task)
		{
			return default(UniTaskVoid);
		}

		[Token(Token = "0x6000795")]
		public Awaiter GetAwaiter()
		{
			return default(Awaiter);
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001A9")]
	[CompilerGenerated]
	private struct _003CRun_003Ed__40
{
		[Token(Token = "0x40006E8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40006E9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40006EA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public bool configureAwait;

		[Token(Token = "0x40006EB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Action action;

		[Token(Token = "0x40006EC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private SwitchToThreadPoolAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40006ED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private object _003C_003E7__wrap1;

		[Token(Token = "0x40006EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private int _003C_003E7__wrap2;

		[Token(Token = "0x40006EF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private YieldAwaitable.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60007AE")]
		[Address(RVA = "0x5178A0", Offset = "0x5162A0", VA = "0x1805178A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007AF")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001AA")]
	[CompilerGenerated]
	private struct _003CRun_003Ed__41
{
		[Token(Token = "0x40006F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40006F1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40006F2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public bool configureAwait;

		[Token(Token = "0x40006F3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Action<object> action;

		[Token(Token = "0x40006F4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public object state;

		[Token(Token = "0x40006F5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private SwitchToThreadPoolAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40006F6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private object _003C_003E7__wrap1;

		[Token(Token = "0x40006F7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private int _003C_003E7__wrap2;

		[Token(Token = "0x40006F8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private YieldAwaitable.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60007B0")]
		[Address(RVA = "0x517D50", Offset = "0x516750", VA = "0x180517D50", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007B1")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001AB")]
	[CompilerGenerated]
	private struct _003CRun_003Ed__42<T>
{
		[Token(Token = "0x40006F9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40006FA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<T> _003C_003Et__builder;

		[Token(Token = "0x40006FB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public bool configureAwait;

		[Token(Token = "0x40006FC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Func<T> func;

		[Token(Token = "0x40006FD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private SwitchToThreadPoolAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40006FE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private object _003C_003E7__wrap1;

		[Token(Token = "0x40006FF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int _003C_003E7__wrap2;

		[Token(Token = "0x4000700")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T _003C_003E7__wrap3;

		[Token(Token = "0x4000701")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private YieldAwaitable.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60007B2")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007B3")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001AC")]
	[CompilerGenerated]
	private struct _003CRun_003Ed__43<T>
{
		[Token(Token = "0x4000702")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000703")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<T> _003C_003Et__builder;

		[Token(Token = "0x4000704")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public bool configureAwait;

		[Token(Token = "0x4000705")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Func<object, T> func;

		[Token(Token = "0x4000706")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public object state;

		[Token(Token = "0x4000707")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private SwitchToThreadPoolAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000708")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private object _003C_003E7__wrap1;

		[Token(Token = "0x4000709")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int _003C_003E7__wrap2;

		[Token(Token = "0x400070A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T _003C_003E7__wrap3;

		[Token(Token = "0x400070B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private YieldAwaitable.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60007B4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007B5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001AD")]
	[CompilerGenerated]
	private struct _003CWhenAll_003Ed__55<T>
{
		[Token(Token = "0x400070C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400070D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<T[]> _003C_003Et__builder;

		[Token(Token = "0x400070E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T>[] tasks;

		[Token(Token = "0x400070F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private WhenAllPromise<T>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007B6")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007B7")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001AE")]
	[CompilerGenerated]
	private struct _003CWhenAll_003Ed__56<T>
{
		[Token(Token = "0x4000710")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000711")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<T[]> _003C_003Et__builder;

		[Token(Token = "0x4000712")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public IEnumerable<UniTask<T>> tasks;

		[Token(Token = "0x4000713")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private WhenAllPromise<T>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007B8")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007B9")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001AF")]
	[CompilerGenerated]
	private struct _003CWhenAll_003Ed__57
{
		[Token(Token = "0x4000714")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000715")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000716")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public UniTask[] tasks;

		[Token(Token = "0x4000717")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private WhenAllPromise.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007BA")]
		[Address(RVA = "0x518210", Offset = "0x516C10", VA = "0x180518210", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007BB")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001B0")]
	[CompilerGenerated]
	private struct _003CWhenAll_003Ed__58
{
		[Token(Token = "0x4000718")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000719")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400071A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public IEnumerable<UniTask> tasks;

		[Token(Token = "0x400071B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private WhenAllPromise.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007BC")]
		[Address(RVA = "0x518440", Offset = "0x516E40", VA = "0x180518440", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007BD")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001B1")]
	[CompilerGenerated]
	private struct _003CWhenAll_003Ed__61<T1, T2>
{
		[Token(Token = "0x400071C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400071D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<(T1 result1, T2 result2)> _003C_003Et__builder;

		[Token(Token = "0x400071E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T1> task1;

		[Token(Token = "0x400071F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T2> task2;

		[Token(Token = "0x4000720")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private WhenAllPromise<T1, T2>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007BE")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007BF")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001B2")]
	[CompilerGenerated]
	private struct _003CWhenAll_003Ed__62<T1, T2, T3>
{
		[Token(Token = "0x4000721")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000722")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<(T1 result1, T2 result2, T3 result3)> _003C_003Et__builder;

		[Token(Token = "0x4000723")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T1> task1;

		[Token(Token = "0x4000724")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T2> task2;

		[Token(Token = "0x4000725")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T3> task3;

		[Token(Token = "0x4000726")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private WhenAllPromise<T1, T2, T3>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007C0")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007C1")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001B3")]
	[CompilerGenerated]
	private struct _003CWhenAll_003Ed__63<T1, T2, T3, T4>
{
		[Token(Token = "0x4000727")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000728")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<(T1 result1, T2 result2, T3 result3, T4 result4)> _003C_003Et__builder;

		[Token(Token = "0x4000729")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T1> task1;

		[Token(Token = "0x400072A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T2> task2;

		[Token(Token = "0x400072B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T3> task3;

		[Token(Token = "0x400072C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T4> task4;

		[Token(Token = "0x400072D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private WhenAllPromise<T1, T2, T3, T4>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007C2")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007C3")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001B4")]
	[CompilerGenerated]
	private struct _003CWhenAll_003Ed__64<T1, T2, T3, T4, T5>
{
		[Token(Token = "0x400072E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400072F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<(T1 result1, T2 result2, T3 result3, T4 result4, T5 result5)> _003C_003Et__builder;

		[Token(Token = "0x4000730")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T1> task1;

		[Token(Token = "0x4000731")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T2> task2;

		[Token(Token = "0x4000732")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T3> task3;

		[Token(Token = "0x4000733")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T4> task4;

		[Token(Token = "0x4000734")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T5> task5;

		[Token(Token = "0x4000735")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private WhenAllPromise<T1, T2, T3, T4, T5>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007C4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007C5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001B5")]
	[CompilerGenerated]
	private struct _003CWhenAll_003Ed__65<T1, T2, T3, T4, T5, T6>
{
		[Token(Token = "0x4000736")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000737")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<(T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6)> _003C_003Et__builder;

		[Token(Token = "0x4000738")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T1> task1;

		[Token(Token = "0x4000739")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T2> task2;

		[Token(Token = "0x400073A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T3> task3;

		[Token(Token = "0x400073B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T4> task4;

		[Token(Token = "0x400073C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T5> task5;

		[Token(Token = "0x400073D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T6> task6;

		[Token(Token = "0x400073E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private WhenAllPromise<T1, T2, T3, T4, T5, T6>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007C6")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007C7")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001B6")]
	[CompilerGenerated]
	private struct _003CWhenAll_003Ed__66<T1, T2, T3, T4, T5, T6, T7>
{
		[Token(Token = "0x400073F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000740")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<(T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7)> _003C_003Et__builder;

		[Token(Token = "0x4000741")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T1> task1;

		[Token(Token = "0x4000742")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T2> task2;

		[Token(Token = "0x4000743")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T3> task3;

		[Token(Token = "0x4000744")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T4> task4;

		[Token(Token = "0x4000745")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T5> task5;

		[Token(Token = "0x4000746")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T6> task6;

		[Token(Token = "0x4000747")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T7> task7;

		[Token(Token = "0x4000748")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007C8")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007C9")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001B7")]
	[CompilerGenerated]
	private struct _003CWhenAny_003Ed__73<T0>
{
		[Token(Token = "0x4000749")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400074A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<(bool hasResultLeft, T0 result)> _003C_003Et__builder;

		[Token(Token = "0x400074B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T0> task0;

		[Token(Token = "0x400074C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask task1;

		[Token(Token = "0x400074D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UnitWhenAnyPromise<T0>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007CA")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007CB")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001B8")]
	[CompilerGenerated]
	private struct _003CWhenAny_003Ed__74<T>
{
		[Token(Token = "0x400074E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400074F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<(int winArgumentIndex, T result)> _003C_003Et__builder;

		[Token(Token = "0x4000750")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T>[] tasks;

		[Token(Token = "0x4000751")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private WhenAnyPromise<T>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007CC")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007CD")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001B9")]
	[CompilerGenerated]
	private struct _003CWhenAny_003Ed__75
{
		[Token(Token = "0x4000752")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000753")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<int> _003C_003Et__builder;

		[Token(Token = "0x4000754")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public UniTask[] tasks;

		[Token(Token = "0x4000755")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private WhenAnyPromise.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007CE")]
		[Address(RVA = "0x518760", Offset = "0x517160", VA = "0x180518760", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007CF")]
		[Address(RVA = "0x518990", Offset = "0x517390", VA = "0x180518990", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001BA")]
	[CompilerGenerated]
	private struct _003CWhenAny_003Ed__79<T0, T1>
{
		[Token(Token = "0x4000756")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000757")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<(int winArgumentIndex, (bool hasResult, T0 result0), (bool hasResult, T1 result1))> _003C_003Et__builder;

		[Token(Token = "0x4000758")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T0> task0;

		[Token(Token = "0x4000759")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T1> task1;

		[Token(Token = "0x400075A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private WhenAnyPromise<T0, T1>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007D0")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007D1")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001BB")]
	[CompilerGenerated]
	private struct _003CWhenAny_003Ed__80<T0, T1, T2>
{
		[Token(Token = "0x400075B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400075C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<(int winArgumentIndex, (bool hasResult, T0 result0), (bool hasResult, T1 result1), (bool hasResult, T2 result2))> _003C_003Et__builder;

		[Token(Token = "0x400075D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T0> task0;

		[Token(Token = "0x400075E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T1> task1;

		[Token(Token = "0x400075F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T2> task2;

		[Token(Token = "0x4000760")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private WhenAnyPromise<T0, T1, T2>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007D2")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007D3")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001BC")]
	[CompilerGenerated]
	private struct _003CWhenAny_003Ed__81<T0, T1, T2, T3>
{
		[Token(Token = "0x4000761")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000762")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<(int winArgumentIndex, (bool hasResult, T0 result0), (bool hasResult, T1 result1), (bool hasResult, T2 result2), (bool hasResult, T3 result3))> _003C_003Et__builder;

		[Token(Token = "0x4000763")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T0> task0;

		[Token(Token = "0x4000764")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T1> task1;

		[Token(Token = "0x4000765")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T2> task2;

		[Token(Token = "0x4000766")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T3> task3;

		[Token(Token = "0x4000767")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private WhenAnyPromise<T0, T1, T2, T3>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007D4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007D5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001BD")]
	[CompilerGenerated]
	private struct _003CWhenAny_003Ed__82<T0, T1, T2, T3, T4>
{
		[Token(Token = "0x4000768")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000769")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<(int winArgumentIndex, (bool hasResult, T0 result0), (bool hasResult, T1 result1), (bool hasResult, T2 result2), (bool hasResult, T3 result3), (bool hasResult, T4 result4))> _003C_003Et__builder;

		[Token(Token = "0x400076A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T0> task0;

		[Token(Token = "0x400076B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T1> task1;

		[Token(Token = "0x400076C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T2> task2;

		[Token(Token = "0x400076D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T3> task3;

		[Token(Token = "0x400076E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T4> task4;

		[Token(Token = "0x400076F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private WhenAnyPromise<T0, T1, T2, T3, T4>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007D6")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007D7")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001BE")]
	[CompilerGenerated]
	private struct _003CWhenAny_003Ed__83<T0, T1, T2, T3, T4, T5>
{
		[Token(Token = "0x4000770")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000771")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<(int winArgumentIndex, (bool hasResult, T0 result0), (bool hasResult, T1 result1), (bool hasResult, T2 result2), (bool hasResult, T3 result3), (bool hasResult, T4 result4), (bool hasResult, T5 result5))> _003C_003Et__builder;

		[Token(Token = "0x4000772")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T0> task0;

		[Token(Token = "0x4000773")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T1> task1;

		[Token(Token = "0x4000774")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T2> task2;

		[Token(Token = "0x4000775")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T3> task3;

		[Token(Token = "0x4000776")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T4> task4;

		[Token(Token = "0x4000777")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T5> task5;

		[Token(Token = "0x4000778")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private WhenAnyPromise<T0, T1, T2, T3, T4, T5>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007D8")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007D9")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001BF")]
	[CompilerGenerated]
	private struct _003CWhenAny_003Ed__84<T0, T1, T2, T3, T4, T5, T6>
{
		[Token(Token = "0x4000779")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400077A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<(int winArgumentIndex, (bool hasResult, T0 result0), (bool hasResult, T1 result1), (bool hasResult, T2 result2), (bool hasResult, T3 result3), (bool hasResult, T4 result4), (bool hasResult, T5 result5), (bool hasResult, T6 result6))> _003C_003Et__builder;

		[Token(Token = "0x400077B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T0> task0;

		[Token(Token = "0x400077C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T1> task1;

		[Token(Token = "0x400077D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T2> task2;

		[Token(Token = "0x400077E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T3> task3;

		[Token(Token = "0x400077F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T4> task4;

		[Token(Token = "0x4000780")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T5> task5;

		[Token(Token = "0x4000781")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T6> task6;

		[Token(Token = "0x4000782")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007DA")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007DB")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001C0")]
	[CompilerGenerated]
	private struct _003CWhenAny_003Ed__85<T0, T1, T2, T3, T4, T5, T6, T7>
{
		[Token(Token = "0x4000783")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000784")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<(int winArgumentIndex, (bool hasResult, T0 result0), (bool hasResult, T1 result1), (bool hasResult, T2 result2), (bool hasResult, T3 result3), (bool hasResult, T4 result4), (bool hasResult, T5 result5), (bool hasResult, T6 result6), (bool hasResult, T7 result7))> _003C_003Et__builder;

		[Token(Token = "0x4000785")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T0> task0;

		[Token(Token = "0x4000786")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T1> task1;

		[Token(Token = "0x4000787")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T2> task2;

		[Token(Token = "0x4000788")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T3> task3;

		[Token(Token = "0x4000789")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T4> task4;

		[Token(Token = "0x400078A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T5> task5;

		[Token(Token = "0x400078B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T6> task6;

		[Token(Token = "0x400078C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public UniTask<T7> task7;

		[Token(Token = "0x400078D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6, T7>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60007DC")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60007DD")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40004DF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly UniTask<AsyncUnit> DefaultAsyncUnitTask;

	[Token(Token = "0x40004E0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly IAwaiter awaiter;

	[Token(Token = "0x40004E1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private static readonly UniTask CanceledUniTask;

	[Token(Token = "0x170000FA")]
	[DebuggerHidden]
	public AwaiterStatus Status
	{
		[Token(Token = "0x60005C8")]
		[Address(RVA = "0x5139A0", Offset = "0x5123A0", VA = "0x1805139A0")]
		get
		{
			return default(AwaiterStatus);
		}
	}

	[Token(Token = "0x170000FB")]
	[DebuggerHidden]
	public bool IsCompleted
	{
		[Token(Token = "0x60005C9")]
		[Address(RVA = "0x5139F0", Offset = "0x5123F0", VA = "0x1805139F0")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x170000FC")]
	public static UniTask CompletedTask
	{
		[Token(Token = "0x60005D5")]
		[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0")]
		get
		{
			return default(UniTask);
		}
	}

	[Token(Token = "0x60005C5")]
	[Address(RVA = "0x513780", Offset = "0x512180", VA = "0x180513780")]
	public static IEnumerator ToCoroutine(Func<UniTask> taskFactory, Action<Exception> exceptionHandler = default)
	{
		return null;
	}

	[Token(Token = "0x60005C6")]
	[Address(RVA = "0x3912B0", Offset = "0x38FCB0", VA = "0x1803912B0")]
	[DebuggerHidden]
	public UniTask(IAwaiter awaiter)
	{
	}

	[Token(Token = "0x60005C7")]
	[Address(RVA = "0x5138A0", Offset = "0x5122A0", VA = "0x1805138A0")]
	[DebuggerHidden]
	public UniTask(Func<UniTask> factory)
	{
	}

	[Token(Token = "0x60005CA")]
	[Address(RVA = "0x513A50", Offset = "0x512450", VA = "0x180513A50")]
	[DebuggerHidden]
	public void GetResult()
	{
	}

	[Token(Token = "0x60005CB")]
	[Address(RVA = "0x513AA0", Offset = "0x5124A0", VA = "0x180513AA0")]
	[DebuggerHidden]
	public Awaiter GetAwaiter()
	{
		return default(Awaiter);
	}

	[Token(Token = "0x60005CC")]
	[Address(RVA = "0x513B00", Offset = "0x512500", VA = "0x180513B00")]
	public UniTask<bool> SuppressCancellationThrow()
	{
		return default(UniTask<bool>);
	}

	[Token(Token = "0x60005CD")]
	[Address(RVA = "0x513CC0", Offset = "0x5126C0", VA = "0x180513CC0", Slot = "4")]
	public bool Equals(UniTask other)
	{
		return default(bool);
	}

	[Token(Token = "0x60005CE")]
	[Address(RVA = "0x477700", Offset = "0x476100", VA = "0x180477700", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}

	[Token(Token = "0x60005CF")]
	[Address(RVA = "0x513CE0", Offset = "0x5126E0", VA = "0x180513CE0", Slot = "3")]
	public override string ToString()
	{
		return null;
	}

	[Token(Token = "0x60005D0")]
	[Address(RVA = "0x513DE0", Offset = "0x5127E0", VA = "0x180513DE0")]
	public static implicit operator UniTask<AsyncUnit>(UniTask task)
	{
		return default(UniTask<AsyncUnit>);
	}

	[Token(Token = "0x60005D1")]
	[Address(RVA = "0x513F30", Offset = "0x512930", VA = "0x180513F30")]
	public static YieldAwaitable Yield(PlayerLoopTiming timing = PlayerLoopTiming.Update, AsyncToken token = default)
	{
		return default(YieldAwaitable);
	}

	[Token(Token = "0x60005D2")]
	[Address(RVA = "0x513FA0", Offset = "0x5129A0", VA = "0x180513FA0")]
	public static UniTask<int> DelayFrame(int delayFrameCount, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default)
	{
		return default(UniTask<int>);
	}

	[Token(Token = "0x60005D3")]
	[Address(RVA = "0x514100", Offset = "0x512B00", VA = "0x180514100")]
	public static UniTask Delay(int millisecondsDelay, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60005D4")]
	[Address(RVA = "0x514300", Offset = "0x512D00", VA = "0x180514300")]
	public static UniTask Delay(TimeSpan delayTimeSpan, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60005D6")]
	[Address(RVA = "0x5144A0", Offset = "0x512EA0", VA = "0x1805144A0")]
	public static UniTask FromException(Exception ex)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60005D7")]
	public static UniTask<T> FromException<T>(Exception ex)
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x60005D8")]
	public static UniTask<T> FromResult<T>(T value)
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x60005D9")]
	[Address(RVA = "0x514580", Offset = "0x512F80", VA = "0x180514580")]
	public static UniTask FromCanceled()
	{
		return default(UniTask);
	}

	[Token(Token = "0x60005DA")]
	public static UniTask<T> FromCanceled<T>()
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x60005DB")]
	[Address(RVA = "0x5145E0", Offset = "0x512FE0", VA = "0x1805145E0")]
	public static UniTask FromCanceled(CancellationToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60005DC")]
	public static UniTask<T> FromCanceled<T>(CancellationToken token)
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x60005DD")]
	public static UniTask<T> Lazy<T>(Func<UniTask<T>> factory)
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x60005DE")]
	[Address(RVA = "0x5146F0", Offset = "0x5130F0", VA = "0x1805146F0")]
	public static void Void(Func<UniTask> asyncAction)
	{
	}

	[Token(Token = "0x60005DF")]
	public static void Void<T>(Func<T, UniTask> asyncAction, T state)
	{
	}

	[Token(Token = "0x60005E0")]
	[Address(RVA = "0x514730", Offset = "0x513130", VA = "0x180514730")]
	[AsyncStateMachine(typeof(_003CRun_003Ed__40))]
	public static UniTask Run(Action action, bool configureAwait = true)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60005E1")]
	[Address(RVA = "0x514880", Offset = "0x513280", VA = "0x180514880")]
	[AsyncStateMachine(typeof(_003CRun_003Ed__41))]
	public static UniTask Run(Action<object> action, object state, bool configureAwait = true)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60005E2")]
	[AsyncStateMachine(typeof(_003CRun_003Ed__42<>))]
	public static UniTask<T> Run<T>(Func<T> func, bool configureAwait = true)
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x60005E3")]
	[AsyncStateMachine(typeof(_003CRun_003Ed__43<>))]
	public static UniTask<T> Run<T>(Func<object, T> func, object state, bool configureAwait = true)
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x60005E4")]
	[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360")]
	public static SwitchToMainThreadAwaitable SwitchToMainThread()
	{
		return default(SwitchToMainThreadAwaitable);
	}

	[Token(Token = "0x60005E5")]
	[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360")]
	public static SwitchToThreadPoolAwaitable SwitchToThreadPool()
	{
		return default(SwitchToThreadPoolAwaitable);
	}

	[Token(Token = "0x60005E6")]
	[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360")]
	public static SwitchToTaskPoolAwaitable SwitchToTaskPool()
	{
		return default(SwitchToTaskPoolAwaitable);
	}

	[Token(Token = "0x60005E7")]
	[Address(RVA = "0x514A20", Offset = "0x513420", VA = "0x180514A20")]
	public static SwitchToSynchronizationContextAwaitable SwitchToSynchronizationContext(SynchronizationContext syncContext)
	{
		return default(SwitchToSynchronizationContextAwaitable);
	}

	[Token(Token = "0x60005E8")]
	[Address(RVA = "0x514AD0", Offset = "0x5134D0", VA = "0x180514AD0")]
	public static UniTask WaitUntil(Func<bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60005E9")]
	[Address(RVA = "0x514C50", Offset = "0x513650", VA = "0x180514C50")]
	public static UniTask WaitWhile(Func<bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60005EA")]
	public static UniTask<U> WaitUntilValueChanged<T, U>(T target, Func<T, U> monitorFunction, PlayerLoopTiming monitorTiming = PlayerLoopTiming.Update, IEqualityComparer<U> equalityComparer = default, CancellationToken cancellationToken = default) where T : class
	{
		return default(UniTask<U>);
	}

	[Token(Token = "0x60005EB")]
	[AsyncStateMachine(typeof(_003CWhenAll_003Ed__55<>))]
	public static UniTask<T[]> WhenAll<T>(params UniTask<T>[] tasks)
	{
		return default(UniTask<T[]>);
	}

	[Token(Token = "0x60005EC")]
	[AsyncStateMachine(typeof(_003CWhenAll_003Ed__56<>))]
	public static UniTask<T[]> WhenAll<T>(IEnumerable<UniTask<T>> tasks)
	{
		return default(UniTask<T[]>);
	}

	[Token(Token = "0x60005ED")]
	[Address(RVA = "0x514DD0", Offset = "0x5137D0", VA = "0x180514DD0")]
	[AsyncStateMachine(typeof(_003CWhenAll_003Ed__57))]
	public static UniTask WhenAll(params UniTask[] tasks)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60005EE")]
	[Address(RVA = "0x514EF0", Offset = "0x5138F0", VA = "0x180514EF0")]
	[AsyncStateMachine(typeof(_003CWhenAll_003Ed__58))]
	public static UniTask WhenAll(IEnumerable<UniTask> tasks)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60005EF")]
	[AsyncStateMachine(typeof(_003CWhenAll_003Ed__61<, >))]
	public static UniTask<(T1, T2)> WhenAll<T1, T2>(UniTask<T1> task1, UniTask<T2> task2)
	{
		return default(UniTask<(T1, T2)>);
	}

	[Token(Token = "0x60005F0")]
	[AsyncStateMachine(typeof(_003CWhenAll_003Ed__62<, , >))]
	public static UniTask<(T1, T2, T3)> WhenAll<T1, T2, T3>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
	{
		return default(UniTask<(T1, T2, T3)>);
	}

	[Token(Token = "0x60005F1")]
	[AsyncStateMachine(typeof(_003CWhenAll_003Ed__63<, , , >))]
	public static UniTask<(T1, T2, T3, T4)> WhenAll<T1, T2, T3, T4>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
	{
		return default(UniTask<(T1, T2, T3, T4)>);
	}

	[Token(Token = "0x60005F2")]
	[AsyncStateMachine(typeof(_003CWhenAll_003Ed__64<, , , , >))]
	public static UniTask<(T1, T2, T3, T4, T5)> WhenAll<T1, T2, T3, T4, T5>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
	{
		return default(UniTask<(T1, T2, T3, T4, T5)>);
	}

	[Token(Token = "0x60005F3")]
	[AsyncStateMachine(typeof(_003CWhenAll_003Ed__65<, , , , , >))]
	public static UniTask<(T1, T2, T3, T4, T5, T6)> WhenAll<T1, T2, T3, T4, T5, T6>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
	{
		return default(UniTask<(T1, T2, T3, T4, T5, T6)>);
	}

	[Token(Token = "0x60005F4")]
	[AsyncStateMachine(typeof(_003CWhenAll_003Ed__66<, , , , , , >))]
	public static UniTask<(T1, T2, T3, T4, T5, T6, T7)> WhenAll<T1, T2, T3, T4, T5, T6, T7>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
	{
		return default(UniTask<(T1, T2, T3, T4, T5, T6, T7)>);
	}

	[Token(Token = "0x60005F5")]
	[AsyncStateMachine(typeof(_003CWhenAny_003Ed__73<>))]
	public static UniTask<(bool, T0)> WhenAny<T0>(UniTask<T0> task0, UniTask task1)
	{
		return default(UniTask<(bool, T0)>);
	}

	[Token(Token = "0x60005F6")]
	[AsyncStateMachine(typeof(_003CWhenAny_003Ed__74<>))]
	public static UniTask<(int, T)> WhenAny<T>(params UniTask<T>[] tasks)
	{
		return default(UniTask<(int, T)>);
	}

	[Token(Token = "0x60005F7")]
	[Address(RVA = "0x515010", Offset = "0x513A10", VA = "0x180515010")]
	[AsyncStateMachine(typeof(_003CWhenAny_003Ed__75))]
	public static UniTask<int> WhenAny(params UniTask[] tasks)
	{
		return default(UniTask<int>);
	}

	[Token(Token = "0x60005F8")]
	[AsyncStateMachine(typeof(_003CWhenAny_003Ed__79<, >))]
	public static UniTask<(int, (bool, T0), (bool, T1))> WhenAny<T0, T1>(UniTask<T0> task0, UniTask<T1> task1)
	{
		return default(UniTask<(int, (bool, T0), (bool, T1))>);
	}

	[Token(Token = "0x60005F9")]
	[AsyncStateMachine(typeof(_003CWhenAny_003Ed__80<, , >))]
	public static UniTask<(int, (bool, T0), (bool, T1), (bool, T2))> WhenAny<T0, T1, T2>(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2)
	{
		return default(UniTask<(int, (bool, T0), (bool, T1), (bool, T2))>);
	}

	[Token(Token = "0x60005FA")]
	[AsyncStateMachine(typeof(_003CWhenAny_003Ed__81<, , , >))]
	public static UniTask<(int, (bool, T0), (bool, T1), (bool, T2), (bool, T3))> WhenAny<T0, T1, T2, T3>(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
	{
		return default(UniTask<(int, (bool, T0), (bool, T1), (bool, T2), (bool, T3))>);
	}

	[Token(Token = "0x60005FB")]
	[AsyncStateMachine(typeof(_003CWhenAny_003Ed__82<, , , , >))]
	public static UniTask<(int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4))> WhenAny<T0, T1, T2, T3, T4>(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
	{
		return default(UniTask<(int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4))>);
	}

	[Token(Token = "0x60005FC")]
	[AsyncStateMachine(typeof(_003CWhenAny_003Ed__83<, , , , , >))]
	public static UniTask<(int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4), (bool, T5))> WhenAny<T0, T1, T2, T3, T4, T5>(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
	{
		return default(UniTask<(int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4), (bool, T5))>);
	}

	[Token(Token = "0x60005FD")]
	[AsyncStateMachine(typeof(_003CWhenAny_003Ed__84<, , , , , , >))]
	public static UniTask<(int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4), (bool, T5), (bool, T6))> WhenAny<T0, T1, T2, T3, T4, T5, T6>(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
	{
		return default(UniTask<(int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4), (bool, T5), (bool, T6))>);
	}

	[Token(Token = "0x60005FE")]
	[AsyncStateMachine(typeof(_003CWhenAny_003Ed__85<, , , , , , , >))]
	public static UniTask<(int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4), (bool, T5), (bool, T6), (bool, T7))> WhenAny<T0, T1, T2, T3, T4, T5, T6, T7>(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
	{
		return default(UniTask<(int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4), (bool, T5), (bool, T6), (bool, T7))>);
	}
}
[Token(Token = "0x20001C1")]
[AsyncMethodBuilder(typeof(AsyncUniTaskMethodBuilder<>))]
public readonly struct UniTask<T> : IEquatable<UniTask<T>>
{
	[Token(Token = "0x20001C2")]
	private class IsCanceledAwaiter : ICriticalNotifyCompletion, INotifyCompletion
{
		[Token(Token = "0x4000790")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private readonly IAwaiter<T> awaiter;

		[Token(Token = "0x17000122")]
		public bool IsCompleted
		{
			[Token(Token = "0x60007EB")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x17000123")]
		public AwaiterStatus Status
		{
			[Token(Token = "0x60007EC")]
			get
			{
				return default(AwaiterStatus);
			}
		}

		[Token(Token = "0x60007EA")]
		public IsCanceledAwaiter(IAwaiter<T> awaiter)
		{
		}

		[Token(Token = "0x60007ED")]
		public (bool, T) GetResult()
		{
			return default((bool, T));
		}

		[Token(Token = "0x60007EE")]
		public void OnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x60007EF")]
		public void UnsafeOnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x60007F0")]
		private void Naninovel_002EAsync_002EIAwaiter_002EGetResult()
		{
		}
	}

	[Token(Token = "0x20001C3")]
	public readonly struct Awaiter : IAwaiter<T>, ICriticalNotifyCompletion, INotifyCompletion
	{
		void IAwaiter.GetResult()
		{
		}
		[Token(Token = "0x4000791")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private readonly UniTask<T> task;

		[Token(Token = "0x17000124")]
		[DebuggerHidden]
		public bool IsCompleted
		{
			[Token(Token = "0x60007F2")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x17000125")]
		[DebuggerHidden]
		public AwaiterStatus Status
		{
			[Token(Token = "0x60007F3")]
			get
			{
				return default(AwaiterStatus);
			}
		}

		[Token(Token = "0x60007F1")]
		[DebuggerHidden]
		public Awaiter(UniTask<T> task)
		{
		}

		[Token(Token = "0x60007F4")]
		[DebuggerHidden]
		private void Naninovel_002EAsync_002EIAwaiter_002EGetResult()
		{
		}

		[Token(Token = "0x60007F5")]
		[DebuggerHidden]
		public T GetResult()
		{
			return default(T);
		}

		[Token(Token = "0x60007F6")]
		[DebuggerHidden]
		public void OnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x60007F7")]
		[DebuggerHidden]
		public void UnsafeOnCompleted(Action continuation)
		{
		}
	}

	[Token(Token = "0x400078E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly T result;

	[Token(Token = "0x400078F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly IAwaiter<T> awaiter;

	[Token(Token = "0x1700011F")]
	[DebuggerHidden]
	public AwaiterStatus Status
	{
		[Token(Token = "0x60007E1")]
		get
		{
			return default(AwaiterStatus);
		}
	}

	[Token(Token = "0x17000120")]
	[DebuggerHidden]
	public bool IsCompleted
	{
		[Token(Token = "0x60007E2")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000121")]
	[DebuggerHidden]
	public T Result
	{
		[Token(Token = "0x60007E3")]
		get
		{
			return default(T);
		}
	}

	[Token(Token = "0x60007DE")]
	[DebuggerHidden]
	public UniTask(T result)
	{
	}

	[Token(Token = "0x60007DF")]
	[DebuggerHidden]
	public UniTask(IAwaiter<T> awaiter)
	{
	}

	[Token(Token = "0x60007E0")]
	[DebuggerHidden]
	public UniTask(Func<UniTask<T>> factory)
	{
	}

	[Token(Token = "0x60007E4")]
	[DebuggerHidden]
	public Awaiter GetAwaiter()
	{
		return default(Awaiter);
	}

	[Token(Token = "0x60007E5")]
	public UniTask<(bool, T)> SuppressCancellationThrow()
	{
		return default(UniTask<(bool, T)>);
	}

	[Token(Token = "0x60007E6")]
	public bool Equals(UniTask<T> other)
	{
		return default(bool);
	}

	[Token(Token = "0x60007E7")]
	public override int GetHashCode()
	{
		return default(int);
	}

	[Token(Token = "0x60007E8")]
	public override string ToString()
	{
		return null;
	}

	[Token(Token = "0x60007E9")]
	public static implicit operator UniTask(UniTask<T> task)
	{
		return default(UniTask);
	}
}
