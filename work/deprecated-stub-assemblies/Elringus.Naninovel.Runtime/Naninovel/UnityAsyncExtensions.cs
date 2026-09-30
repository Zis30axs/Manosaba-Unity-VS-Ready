using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Il2CppDummyDll;
using Naninovel.Async;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20001F6")]
    public static class UnityAsyncExtensions
    {
    	[Token(Token = "0x20001F7")]
    	public struct AsyncOperationAwaiter : IAwaiter, ICriticalNotifyCompletion, INotifyCompletion
    	{
    		[Token(Token = "0x4000855")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		private AsyncOperation asyncOperation;

    		[Token(Token = "0x4000856")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		private Action<AsyncOperation> continuationAction;

    		[Token(Token = "0x17000135")]
    		public bool IsCompleted
    		{
    			[Token(Token = "0x60008AE")]
    			[Address(RVA = "0x51E230", Offset = "0x51CC30", VA = "0x18051E230", Slot = "5")]
    			get
    			{
    				return default(bool);
    			}
    		}

    		[Token(Token = "0x17000136")]
    		public AwaiterStatus Status
    		{
    			[Token(Token = "0x60008AF")]
    			[Address(RVA = "0x437AC0", Offset = "0x4364C0", VA = "0x180437AC0", Slot = "4")]
    			[CompilerGenerated]
    			readonly get
    			{
    				return default(AwaiterStatus);
    			}
    			[Token(Token = "0x60008B0")]
    			[Address(RVA = "0x438030", Offset = "0x436A30", VA = "0x180438030")]
    			[CompilerGenerated]
    			private set
    			{
    			}
    		}

    		[Token(Token = "0x60008B1")]
    		[Address(RVA = "0x51E240", Offset = "0x51CC40", VA = "0x18051E240")]
    		public AsyncOperationAwaiter(AsyncOperation asyncOperation)
    		{ this = default; }

    		[Token(Token = "0x60008B2")]
    		[Address(RVA = "0x51E3A0", Offset = "0x51CDA0", VA = "0x18051E3A0", Slot = "6")]
    		public void GetResult()
    		{
    		}

    		[Token(Token = "0x60008B3")]
    		[Address(RVA = "0x51E580", Offset = "0x51CF80", VA = "0x18051E580", Slot = "8")]
    		public void OnCompleted(Action continuation)
    		{
    		}

    		[Token(Token = "0x60008B4")]
    		[Address(RVA = "0x51E590", Offset = "0x51CF90", VA = "0x18051E590", Slot = "7")]
    		public void UnsafeOnCompleted(Action continuation)
    		{
    		}
    	}

    	[Token(Token = "0x20001F8")]
    	private class AsyncOperationConfiguredAwaiter : IAwaiter, ICriticalNotifyCompletion, INotifyCompletion, IPlayerLoopItem
    	{
    		[Token(Token = "0x4000858")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		private AsyncOperation asyncOperation;

    		[Token(Token = "0x4000859")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private IProgress<float> progress;

    		[Token(Token = "0x400085A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private CancellationToken cancellationToken;

    		[Token(Token = "0x400085B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private Action continuation;

    		[Token(Token = "0x17000137")]
    		public bool IsCompleted
    		{
    			[Token(Token = "0x60008B5")]
    			[Address(RVA = "0x519A80", Offset = "0x518480", VA = "0x180519A80", Slot = "5")]
    			get
    			{
    				return default(bool);
    			}
    		}

    		[Token(Token = "0x17000138")]
    		public AwaiterStatus Status
    		{
    			[Token(Token = "0x60008B6")]
    			[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650", Slot = "4")]
    			[CompilerGenerated]
    			get
    			{
    				return default(AwaiterStatus);
    			}
    			[Token(Token = "0x60008B7")]
    			[Address(RVA = "0x397FF0", Offset = "0x3969F0", VA = "0x180397FF0")]
    			[CompilerGenerated]
    			private set
    			{
    			}
    		}

    		[Token(Token = "0x60008B8")]
    		[Address(RVA = "0x51E690", Offset = "0x51D090", VA = "0x18051E690")]
    		public AsyncOperationConfiguredAwaiter(AsyncOperation asyncOperation, IProgress<float> progress, CancellationToken cancellationToken)
    		{
    		}

    		[Token(Token = "0x60008B9")]
    		[Address(RVA = "0x51E8F0", Offset = "0x51D2F0", VA = "0x18051E8F0", Slot = "6")]
    		public void GetResult()
    		{
    		}

    		[Token(Token = "0x60008BA")]
    		[Address(RVA = "0x51E920", Offset = "0x51D320", VA = "0x18051E920", Slot = "9")]
    		public bool MoveNext()
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x60008BB")]
    		[Address(RVA = "0x51EAD0", Offset = "0x51D4D0", VA = "0x18051EAD0")]
    		private void InvokeContinuation(AwaiterStatus status)
    		{
    		}

    		[Token(Token = "0x60008BC")]
    		[Address(RVA = "0x51ECB0", Offset = "0x51D6B0", VA = "0x18051ECB0", Slot = "8")]
    		public void OnCompleted(Action continuation)
    		{
    		}

    		[Token(Token = "0x60008BD")]
    		[Address(RVA = "0x51ECB0", Offset = "0x51D6B0", VA = "0x18051ECB0", Slot = "7")]
    		public void UnsafeOnCompleted(Action continuation)
    		{
    		}
    	}

    	[Token(Token = "0x20001F9")]
    	public struct ResourceRequestAwaiter
    {
    		[Token(Token = "0x400085D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		private ResourceRequest asyncOperation;

    		[Token(Token = "0x400085E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		private Action<AsyncOperation> continuationAction;

    		[Token(Token = "0x400085F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		private UnityEngine.Object result;

    		[Token(Token = "0x17000139")]
    		public bool IsCompleted
    		{
    			[Token(Token = "0x60008BE")]
    			[Address(RVA = "0x51E230", Offset = "0x51CC30", VA = "0x18051E230", Slot = "6")]
    			get
    			{
    				return default(bool);
    			}
    		}

    		[Token(Token = "0x1700013A")]
    		public AwaiterStatus Status
    		{
    			[Token(Token = "0x60008BF")]
    			[Address(RVA = "0x437AC0", Offset = "0x4364C0", VA = "0x180437AC0", Slot = "5")]
    			[CompilerGenerated]
    			readonly get
    			{
    				return default(AwaiterStatus);
    			}
    			[Token(Token = "0x60008C0")]
    			[Address(RVA = "0x438030", Offset = "0x436A30", VA = "0x180438030")]
    			[CompilerGenerated]
    			private set
    			{
    			}
    		}

    		[Token(Token = "0x60008C1")]
    		[Address(RVA = "0x51ED80", Offset = "0x51D780", VA = "0x18051ED80")]
    		public ResourceRequestAwaiter(ResourceRequest asyncOperation)
    		{ this = default; }

    		[Token(Token = "0x60008C2")]
    		[Address(RVA = "0x51EF40", Offset = "0x51D940", VA = "0x18051EF40", Slot = "4")]
    		public UnityEngine.Object GetResult()
    		{
    			return null;
    		}

    		[Token(Token = "0x60008C3")]
    		[Address(RVA = "0x51F1A0", Offset = "0x51DBA0", VA = "0x18051F1A0", Slot = "7")]
    		private void Naninovel_002EAsync_002EIAwaiter_002EGetResult()
    		{
    		}

    		[Token(Token = "0x60008C4")]
    		[Address(RVA = "0x51F1B0", Offset = "0x51DBB0", VA = "0x18051F1B0", Slot = "9")]
    		public void OnCompleted(Action continuation)
    		{
    		}

    		[Token(Token = "0x60008C5")]
    		[Address(RVA = "0x51F1C0", Offset = "0x51DBC0", VA = "0x18051F1C0", Slot = "8")]
    		public void UnsafeOnCompleted(Action continuation)
    		{
    		}
    	}

    	[Token(Token = "0x20001FA")]
    	private class ResourceRequestConfiguredAwaiter
    {
    		[Token(Token = "0x4000861")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		private ResourceRequest asyncOperation;

    		[Token(Token = "0x4000862")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private IProgress<float> progress;

    		[Token(Token = "0x4000863")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private CancellationToken cancellationToken;

    		[Token(Token = "0x4000864")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private Action continuation;

    		[Token(Token = "0x4000865")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UnityEngine.Object result;

    		[Token(Token = "0x1700013B")]
    		public bool IsCompleted
    		{
    			[Token(Token = "0x60008C6")]
    			[Address(RVA = "0x519A80", Offset = "0x518480", VA = "0x180519A80", Slot = "6")]
    			get
    			{
    				return default(bool);
    			}
    		}

    		[Token(Token = "0x1700013C")]
    		public AwaiterStatus Status
    		{
    			[Token(Token = "0x60008C7")]
    			[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650", Slot = "5")]
    			[CompilerGenerated]
    			get
    			{
    				return default(AwaiterStatus);
    			}
    			[Token(Token = "0x60008C8")]
    			[Address(RVA = "0x397FF0", Offset = "0x3969F0", VA = "0x180397FF0")]
    			[CompilerGenerated]
    			private set
    			{
    			}
    		}

    		[Token(Token = "0x60008C9")]
    		[Address(RVA = "0x51F2C0", Offset = "0x51DCC0", VA = "0x18051F2C0")]
    		public ResourceRequestConfiguredAwaiter(ResourceRequest asyncOperation, IProgress<float> progress, CancellationToken cancellationToken)
    		{
    		}

    		[Token(Token = "0x60008CA")]
    		[Address(RVA = "0x51F620", Offset = "0x51E020", VA = "0x18051F620", Slot = "7")]
    		private void Naninovel_002EAsync_002EIAwaiter_002EGetResult()
    		{
    		}

    		[Token(Token = "0x60008CB")]
    		[Address(RVA = "0x51F670", Offset = "0x51E070", VA = "0x18051F670", Slot = "4")]
    		public UnityEngine.Object GetResult()
    		{
    			return null;
    		}

    		[Token(Token = "0x60008CC")]
    		[Address(RVA = "0x51F6C0", Offset = "0x51E0C0", VA = "0x18051F6C0", Slot = "10")]
    		public bool MoveNext()
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x60008CD")]
    		[Address(RVA = "0x51F8E0", Offset = "0x51E2E0", VA = "0x18051F8E0")]
    		private void InvokeContinuation(AwaiterStatus status)
    		{
    		}

    		[Token(Token = "0x60008CE")]
    		[Address(RVA = "0x51FAC0", Offset = "0x51E4C0", VA = "0x18051FAC0", Slot = "9")]
    		public void OnCompleted(Action continuation)
    		{
    		}

    		[Token(Token = "0x60008CF")]
    		[Address(RVA = "0x51FB90", Offset = "0x51E590", VA = "0x18051FB90", Slot = "8")]
    		public void UnsafeOnCompleted(Action continuation)
    		{
    		}
    	}

    	[Token(Token = "0x60008A8")]
    	[Address(RVA = "0x51DCA0", Offset = "0x51C6A0", VA = "0x18051DCA0")]
    	public static AsyncOperationAwaiter GetAwaiter(this AsyncOperation asyncOperation)
    	{
    		return default(AsyncOperationAwaiter);
    	}

    	[Token(Token = "0x60008A9")]
    	[Address(RVA = "0x51DD20", Offset = "0x51C720", VA = "0x18051DD20")]
    	public static UniTask ToUniTask(this AsyncOperation asyncOperation)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60008AA")]
    	[Address(RVA = "0x51DE30", Offset = "0x51C830", VA = "0x18051DE30")]
    	public static UniTask ConfigureAwait(this AsyncOperation asyncOperation, IProgress<float> progress = default, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellation = default)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60008AB")]
    	[Address(RVA = "0x51DF90", Offset = "0x51C990", VA = "0x18051DF90")]
    	public static ResourceRequestAwaiter GetAwaiter(this ResourceRequest resourceRequest)
    	{
    		return default(ResourceRequestAwaiter);
    	}

    	[Token(Token = "0x60008AC")]
    	[Address(RVA = "0x51E010", Offset = "0x51CA10", VA = "0x18051E010")]
    	public static UniTask<UnityEngine.Object> ToUniTask(this ResourceRequest resourceRequest)
    	{
    		return default(UniTask<UnityEngine.Object>);
    	}

    	[Token(Token = "0x60008AD")]
    	[Address(RVA = "0x51E0F0", Offset = "0x51CAF0", VA = "0x18051E0F0")]
    	public static UniTask<UnityEngine.Object> ConfigureAwait(this ResourceRequest resourceRequest, IProgress<float> progress = default, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellation = default)
    	{
    		return default(UniTask<UnityEngine.Object>);
    	}
    }
}
