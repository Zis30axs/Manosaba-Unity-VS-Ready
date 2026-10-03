using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.Input
{
    [Token(Token = "0x200000A")]
    public static class ResponseExtensions
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200000B")]
    	[CompilerGenerated]
    	private struct _003CToCancel_003Ed__0
    {
    		[Token(Token = "0x400000D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400000E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<ResponseKind> _003C_003Et__builder;

    		[Token(Token = "0x400000F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public UniTask task;

    		[Token(Token = "0x4000010")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600001D")]
    		[Address(RVA = "0xCD0C10", Offset = "0xCCF610", VA = "0x180CD0C10", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600001E")]
    		[Address(RVA = "0xCD0EB0", Offset = "0xCCF8B0", VA = "0x180CD0EB0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x600001C")]
    	[Address(RVA = "0xCD0A80", Offset = "0xCCF480", VA = "0x180CD0A80")]
        public static async UniTask<ResponseKind> ToCancel(this UniTask task)
        {
            await task;
            return ResponseKind.Cancel;
    	}
    }
}
