using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace GigaCreation.NaninovelExtender.Common;

[Token(Token = "0x200000F")]
[UsedImplicitly]
public class WaitForNextFrame : Command
{
	[StructLayout(3)]
	[Token(Token = "0x2000010")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__0
{
		[Token(Token = "0x400002F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000030")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000031")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AsyncToken token;

		[Token(Token = "0x4000032")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600001F")]
		[Address(RVA = "0xCFA1F0", Offset = "0xCF8BF0", VA = "0x180CFA1F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000020")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x600001D")]
	[Address(RVA = "0xCFA0D0", Offset = "0xCF8AD0", VA = "0x180CFA0D0", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__0))]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600001E")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public WaitForNextFrame()
	{
	}
}
