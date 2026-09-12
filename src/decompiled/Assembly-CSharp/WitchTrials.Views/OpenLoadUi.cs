using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace WitchTrials.Views;

[Token(Token = "0x2000035")]
[UsedImplicitly]
public class OpenLoadUi : Command
{
	[StructLayout(3)]
	[Token(Token = "0x2000036")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__0
{
		[Token(Token = "0x4000090")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000091")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000092")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AsyncToken token;

		[Token(Token = "0x4000093")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60000BB")]
		[Address(RVA = "0x319670", Offset = "0x318070", VA = "0x180319670", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000BC")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x60000B9")]
	[Address(RVA = "0x319550", Offset = "0x317F50", VA = "0x180319550", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__0))]
	public override Naninovel.UniTask Execute([Optional] AsyncToken token)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x60000BA")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public OpenLoadUi()
	{
	}
}
