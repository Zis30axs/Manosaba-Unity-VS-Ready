using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;

namespace Naninovel
{
    [Token(Token = "0x20004C7")]
    public class LoadingScreen : IDisposable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20004C8")]
    	[CompilerGenerated]
    	private struct _003CShow_003Ed__2
    {
    		[Token(Token = "0x4001225")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001226")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<IDisposable> _003C_003Et__builder;

    		[Token(Token = "0x4001227")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001228")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private LoadingScreen _003Cscreen_003E5__2;

    		[Token(Token = "0x4001229")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60017E7")]
    		[Address(RVA = "0x5FB470", Offset = "0x5F9E70", VA = "0x1805FB470", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60017E8")]
    		[Address(RVA = "0x5FB990", Offset = "0x5FA390", VA = "0x1805FB990", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001224")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private readonly ILoadingUI loadingUI;

    	[Token(Token = "0x60017E4")]
    	[Address(RVA = "0x5FB190", Offset = "0x5F9B90", VA = "0x1805FB190")]
    	private LoadingScreen()
    	{
    	}

    	[Token(Token = "0x60017E5")]
    	[Address(RVA = "0x5FB270", Offset = "0x5F9C70", VA = "0x1805FB270")]
    	[AsyncStateMachine(typeof(_003CShow_003Ed__2))]
    	public static UniTask<IDisposable> Show([Optional] AsyncToken token)
    	{
    		return default(UniTask<IDisposable>);
    	}

    	[Token(Token = "0x60017E6")]
    	[Address(RVA = "0x5FB400", Offset = "0x5F9E00", VA = "0x1805FB400", Slot = "4")]
    	public void Dispose()
    	{
    	}
    }
}
