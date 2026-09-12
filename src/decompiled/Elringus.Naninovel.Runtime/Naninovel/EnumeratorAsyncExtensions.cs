using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using Il2CppDummyDll;
using Naninovel.Async;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x200012C")]
public static class EnumeratorAsyncExtensions
{
	[Token(Token = "0x200012D")]
	private class EnumeratorAwaiter : IAwaiter, ICriticalNotifyCompletion, INotifyCompletion, IPlayerLoopItem
	{
		[Token(Token = "0x200012E")]
		[CompilerGenerated]
		private sealed class _003CConsumeEnumerator_003Ed__15
{
			[Token(Token = "0x40004AB")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
			private int _003C_003E1__state;

			[Token(Token = "0x40004AC")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
			private object _003C_003E2__current;

			[Token(Token = "0x40004AD")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
			public IEnumerator enumerator;

			[Token(Token = "0x40004AE")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
			private IEnumerator _003Ce2_003E5__2;

			[Token(Token = "0x170000F2")]
			private object System_002ECollections_002EGeneric_002EIEnumerator_003CSystem_002EObject_003E_002ECurrent
			{
				[Token(Token = "0x60005B0")]
				[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "4")]
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[Token(Token = "0x170000F3")]
			private object System_002ECollections_002EIEnumerator_002ECurrent
			{
				[Token(Token = "0x60005B2")]
				[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "7")]
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[Token(Token = "0x60005AD")]
			[Address(RVA = "0x397FF0", Offset = "0x3969F0", VA = "0x180397FF0")]
			[DebuggerHidden]
			public _003CConsumeEnumerator_003Ed__15(int _003C_003E1__state)
			{
			}

			[Token(Token = "0x60005AE")]
			[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
			[DebuggerHidden]
			private void System_002EIDisposable_002EDispose()
			{
			}

			[Token(Token = "0x60005AF")]
			[Address(RVA = "0x512D20", Offset = "0x511720", VA = "0x180512D20", Slot = "6")]
			private bool MoveNext()
			{
				return default(bool);
			}

			[Token(Token = "0x60005B1")]
			[Address(RVA = "0x513220", Offset = "0x511C20", VA = "0x180513220", Slot = "8")]
			[DebuggerHidden]
			private void System_002ECollections_002EIEnumerator_002EReset()
			{
			}
		}

		[Token(Token = "0x200012F")]
		[CompilerGenerated]
		private sealed class _003CUnwrapWaitAsyncOperation_003Ed__19
{
			[Token(Token = "0x40004AF")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
			private int _003C_003E1__state;

			[Token(Token = "0x40004B0")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
			private object _003C_003E2__current;

			[Token(Token = "0x40004B1")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
			public AsyncOperation asyncOperation;

			[Token(Token = "0x170000F4")]
			private object System_002ECollections_002EGeneric_002EIEnumerator_003CSystem_002EObject_003E_002ECurrent
			{
				[Token(Token = "0x60005B6")]
				[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "4")]
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[Token(Token = "0x170000F5")]
			private object System_002ECollections_002EIEnumerator_002ECurrent
			{
				[Token(Token = "0x60005B8")]
				[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "7")]
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[Token(Token = "0x60005B3")]
			[Address(RVA = "0x397FF0", Offset = "0x3969F0", VA = "0x180397FF0")]
			[DebuggerHidden]
			public _003CUnwrapWaitAsyncOperation_003Ed__19(int _003C_003E1__state)
			{
			}

			[Token(Token = "0x60005B4")]
			[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
			[DebuggerHidden]
			private void System_002EIDisposable_002EDispose()
			{
			}

			[Token(Token = "0x60005B5")]
			[Address(RVA = "0x513260", Offset = "0x511C60", VA = "0x180513260", Slot = "6")]
			private bool MoveNext()
			{
				return default(bool);
			}

			[Token(Token = "0x60005B7")]
			[Address(RVA = "0x513370", Offset = "0x511D70", VA = "0x180513370", Slot = "8")]
			[DebuggerHidden]
			private void System_002ECollections_002EIEnumerator_002EReset()
			{
			}
		}

		[Token(Token = "0x2000130")]
		[CompilerGenerated]
		private sealed class _003CUnwrapWaitCustomYieldInstruction_003Ed__16
{
			[Token(Token = "0x40004B2")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
			private int _003C_003E1__state;

			[Token(Token = "0x40004B3")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
			private object _003C_003E2__current;

			[Token(Token = "0x40004B4")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
			public CustomYieldInstruction yieldInstruction;

			[Token(Token = "0x170000F6")]
			private object System_002ECollections_002EGeneric_002EIEnumerator_003CSystem_002EObject_003E_002ECurrent
			{
				[Token(Token = "0x60005BC")]
				[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "4")]
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[Token(Token = "0x170000F7")]
			private object System_002ECollections_002EIEnumerator_002ECurrent
			{
				[Token(Token = "0x60005BE")]
				[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "7")]
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[Token(Token = "0x60005B9")]
			[Address(RVA = "0x397FF0", Offset = "0x3969F0", VA = "0x180397FF0")]
			[DebuggerHidden]
			public _003CUnwrapWaitCustomYieldInstruction_003Ed__16(int _003C_003E1__state)
			{
			}

			[Token(Token = "0x60005BA")]
			[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
			[DebuggerHidden]
			private void System_002EIDisposable_002EDispose()
			{
			}

			[Token(Token = "0x60005BB")]
			[Address(RVA = "0x5133B0", Offset = "0x511DB0", VA = "0x1805133B0", Slot = "6")]
			private bool MoveNext()
			{
				return default(bool);
			}

			[Token(Token = "0x60005BD")]
			[Address(RVA = "0x513460", Offset = "0x511E60", VA = "0x180513460", Slot = "8")]
			[DebuggerHidden]
			private void System_002ECollections_002EIEnumerator_002EReset()
			{
			}
		}

		[Token(Token = "0x2000131")]
		[CompilerGenerated]
		private sealed class _003CUnwrapWaitForSeconds_003Ed__18
{
			[Token(Token = "0x40004B5")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
			private int _003C_003E1__state;

			[Token(Token = "0x40004B6")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
			private object _003C_003E2__current;

			[Token(Token = "0x40004B7")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
			public WaitForSeconds waitForSeconds;

			[Token(Token = "0x40004B8")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
			private float _003Csecond_003E5__2;

			[Token(Token = "0x40004B9")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
			private DateTimeOffset _003CstartTime_003E5__3;

			[Token(Token = "0x170000F8")]
			private object System_002ECollections_002EGeneric_002EIEnumerator_003CSystem_002EObject_003E_002ECurrent
			{
				[Token(Token = "0x60005C2")]
				[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "4")]
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[Token(Token = "0x170000F9")]
			private object System_002ECollections_002EIEnumerator_002ECurrent
			{
				[Token(Token = "0x60005C4")]
				[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "7")]
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[Token(Token = "0x60005BF")]
			[Address(RVA = "0x397FF0", Offset = "0x3969F0", VA = "0x180397FF0")]
			[DebuggerHidden]
			public _003CUnwrapWaitForSeconds_003Ed__18(int _003C_003E1__state)
			{
			}

			[Token(Token = "0x60005C0")]
			[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
			[DebuggerHidden]
			private void System_002EIDisposable_002EDispose()
			{
			}

			[Token(Token = "0x60005C1")]
			[Address(RVA = "0x5134A0", Offset = "0x511EA0", VA = "0x1805134A0", Slot = "6")]
			private bool MoveNext()
			{
				return default(bool);
			}

			[Token(Token = "0x60005C3")]
			[Address(RVA = "0x513740", Offset = "0x512140", VA = "0x180513740", Slot = "8")]
			[DebuggerHidden]
			private void System_002ECollections_002EIEnumerator_002EReset()
			{
			}
		}

		[Token(Token = "0x40004A5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		private IEnumerator innerEnumerator;

		[Token(Token = "0x40004A6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		private CancellationToken cancellationToken;

		[Token(Token = "0x40004A7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private Action continuation;

		[Token(Token = "0x40004A8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private AwaiterStatus status;

		[Token(Token = "0x40004A9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private ExceptionDispatchInfo exception;

		[Token(Token = "0x40004AA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private static readonly FieldInfo waitForSeconds_Seconds;

		[Token(Token = "0x170000F0")]
		public bool IsCompleted
		{
			[Token(Token = "0x60005A1")]
			[Address(RVA = "0x512500", Offset = "0x510F00", VA = "0x180512500", Slot = "5")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x170000F1")]
		public AwaiterStatus Status
		{
			[Token(Token = "0x60005A2")]
			[Address(RVA = "0x4403C0", Offset = "0x43EDC0", VA = "0x1804403C0", Slot = "4")]
			get
			{
				return default(AwaiterStatus);
			}
		}

		[Token(Token = "0x60005A0")]
		[Address(RVA = "0x512340", Offset = "0x510D40", VA = "0x180512340")]
		public EnumeratorAwaiter(IEnumerator innerEnumerator, CancellationToken cancellationToken)
		{
		}

		[Token(Token = "0x60005A3")]
		[Address(RVA = "0x512510", Offset = "0x510F10", VA = "0x180512510", Slot = "6")]
		public void GetResult()
		{
		}

		[Token(Token = "0x60005A4")]
		[Address(RVA = "0x512560", Offset = "0x510F60", VA = "0x180512560", Slot = "9")]
		public bool MoveNext()
		{
			return default(bool);
		}

		[Token(Token = "0x60005A5")]
		[Address(RVA = "0x512710", Offset = "0x511110", VA = "0x180512710")]
		private void InvokeContinuation(AwaiterStatus status)
		{
		}

		[Token(Token = "0x60005A6")]
		[Address(RVA = "0x5128A0", Offset = "0x5112A0", VA = "0x1805128A0", Slot = "8")]
		public void OnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x60005A7")]
		[Address(RVA = "0x5128A0", Offset = "0x5112A0", VA = "0x1805128A0", Slot = "7")]
		public void UnsafeOnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x60005A8")]
		[Address(RVA = "0x512970", Offset = "0x511370", VA = "0x180512970")]
		[IteratorStateMachine(typeof(_003CConsumeEnumerator_003Ed__15))]
		private static IEnumerator ConsumeEnumerator(IEnumerator enumerator)
		{
			return null;
		}

		[Token(Token = "0x60005A9")]
		[Address(RVA = "0x512A10", Offset = "0x511410", VA = "0x180512A10")]
		[IteratorStateMachine(typeof(_003CUnwrapWaitCustomYieldInstruction_003Ed__16))]
		private static IEnumerator UnwrapWaitCustomYieldInstruction(CustomYieldInstruction yieldInstruction)
		{
			return null;
		}

		[Token(Token = "0x60005AA")]
		[Address(RVA = "0x512AB0", Offset = "0x5114B0", VA = "0x180512AB0")]
		[IteratorStateMachine(typeof(_003CUnwrapWaitForSeconds_003Ed__18))]
		private static IEnumerator UnwrapWaitForSeconds(WaitForSeconds waitForSeconds)
		{
			return null;
		}

		[Token(Token = "0x60005AB")]
		[Address(RVA = "0x512B50", Offset = "0x511550", VA = "0x180512B50")]
		[IteratorStateMachine(typeof(_003CUnwrapWaitAsyncOperation_003Ed__19))]
		private static IEnumerator UnwrapWaitAsyncOperation(AsyncOperation asyncOperation)
		{
			return null;
		}
	}

	[Token(Token = "0x600059D")]
	[Address(RVA = "0x512030", Offset = "0x510A30", VA = "0x180512030")]
	public static IAwaiter GetAwaiter(this IEnumerator enumerator)
	{
		return null;
	}

	[Token(Token = "0x600059E")]
	[Address(RVA = "0x512100", Offset = "0x510B00", VA = "0x180512100")]
	public static UniTask ToUniTask(this IEnumerator enumerator)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600059F")]
	[Address(RVA = "0x512220", Offset = "0x510C20", VA = "0x180512220")]
	public static UniTask ConfigureAwait(this IEnumerator enumerator, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default)
	{
		return default(UniTask);
	}
}
