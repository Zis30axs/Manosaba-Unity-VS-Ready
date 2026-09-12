using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using JetBrains.Annotations;

namespace GigaCreation.Tools.UniTasks;

[Token(Token = "0x2000005")]
[UsedImplicitly(ImplicitUseTargetFlags.Members)]
public class SimpleWaitable : ISimpleWaitable
{
	[StructLayout(3)]
	[Token(Token = "0x2000006")]
	[CompilerGenerated]
	private struct _003CWaitForCompletionAsync_003Ed__3
{
		[Token(Token = "0x4000007")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000008")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<float> _003C_003Et__builder;

		[Token(Token = "0x4000009")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x400000A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public SimpleWaitable _003C_003E4__this;

		[Token(Token = "0x400000B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private float _003CtimeRequestedToPresent_003E5__2;

		[Token(Token = "0x400000C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000009")]
		[Address(RVA = "0xD5D750", Offset = "0xD5C150", VA = "0x180D5D750", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600000A")]
		[Address(RVA = "0xD5DBA0", Offset = "0xD5C5A0", VA = "0x180D5DBA0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000006")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private readonly UniTaskCompletionSource _ucs;

	[Token(Token = "0x17000002")]
	public bool IsPending
	{
		[Token(Token = "0x6000005")]
		[Address(RVA = "0xD5D380", Offset = "0xD5BD80", VA = "0x180D5D380", Slot = "4")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x6000006")]
	[Address(RVA = "0xD5D4A0", Offset = "0xD5BEA0", VA = "0x180D5D4A0", Slot = "5")]
	[AsyncStateMachine(typeof(_003CWaitForCompletionAsync_003Ed__3))]
	public UniTask<float> WaitForCompletionAsync([Optional] CancellationToken ct)
	{
		return default(UniTask<float>);
	}

	[Token(Token = "0x6000007")]
	[Address(RVA = "0xD5D690", Offset = "0xD5C090", VA = "0x180D5D690")]
	public void Completed()
	{
	}

	[Token(Token = "0x6000008")]
	[Address(RVA = "0xD5D6C0", Offset = "0xD5C0C0", VA = "0x180D5D6C0")]
	public SimpleWaitable()
	{
	}
}
