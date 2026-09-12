using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Il2CppDummyDll;

namespace Naninovel.Bridging;

[Token(Token = "0x20000D2")]
internal class Transport
{
	[StructLayout(3)]
	[Token(Token = "0x20000D6")]
	[CompilerGenerated]
	private struct _003CCollectStale_003Ed__12
{
		[Token(Token = "0x4000276")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000277")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000278")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Transport _003C_003E4__this;

		[Token(Token = "0x4000279")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private IEnumerator<string> _003C_003E7__wrap1;

		[Token(Token = "0x400027A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private TaskAwaiter<IReadOnlyCollection<string>> _003C_003Eu__1;

		[Token(Token = "0x400027B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private TaskAwaiter<string> _003C_003Eu__2;

		[Token(Token = "0x600043D")]
		[Address(RVA = "0x11067E0", Offset = "0x11051E0", VA = "0x1811067E0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600043E")]
		[Address(RVA = "0x1107110", Offset = "0x1105B10", VA = "0x181107110", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000D7")]
	[CompilerGenerated]
	private struct _003CSend_003Ed__9
{
		[Token(Token = "0x400027C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400027D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400027E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public Message message;

		[Token(Token = "0x400027F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		public Transport _003C_003E4__this;

		[Token(Token = "0x4000280")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		public Recipient recipient;

		[Token(Token = "0x4000281")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private TaskAwaiter _003C_003Eu__1;

		[Token(Token = "0x600043F")]
		[Address(RVA = "0x1107170", Offset = "0x1105B70", VA = "0x181107170", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000440")]
		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000266")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	[CompilerGenerated]
	private string _003Cid_003EP;

	[Token(Token = "0x4000267")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	[CompilerGenerated]
	private IFiles _003Cfiles_003EP;

	[Token(Token = "0x4000268")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[CompilerGenerated]
	private ISerializer _003Cserde_003EP;

	[Token(Token = "0x4000269")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private readonly List<Message> sent;

	[Token(Token = "0x400026A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	private readonly HashSet<string> stale;

	[Token(Token = "0x400026B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private Task writing;

	[Token(Token = "0x400026C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private bool writeQueued;

	[Token(Token = "0x6000430")]
	[Address(RVA = "0x1105560", Offset = "0x1103F60", VA = "0x181105560")]
	public Transport(string id, IFiles files, ISerializer serde)
	{
	}

	[Token(Token = "0x6000431")]
	[Address(RVA = "0x11058A0", Offset = "0x11042A0", VA = "0x1811058A0")]
	public void Listen(Recipient recipient, Action<Message> handleMessage)
	{
	}

	[Token(Token = "0x6000432")]
	[Address(RVA = "0x1105BC0", Offset = "0x11045C0", VA = "0x181105BC0")]
	[AsyncStateMachine(typeof(_003CSend_003Ed__9))]
	public void Send(Recipient recipient, Message message)
	{
	}

	[Token(Token = "0x6000433")]
	[Address(RVA = "0x1105DA0", Offset = "0x11047A0", VA = "0x181105DA0")]
	private bool IsClient(string name)
	{
		return default(bool);
	}

	[Token(Token = "0x6000434")]
	[Address(RVA = "0x1105DF0", Offset = "0x11047F0", VA = "0x181105DF0")]
	private bool IsServer(string name)
	{
		return default(bool);
	}

	[Token(Token = "0x6000435")]
	[Address(RVA = "0x1105E70", Offset = "0x1104870", VA = "0x181105E70")]
	[AsyncStateMachine(typeof(_003CCollectStale_003Ed__12))]
	private Task CollectStale()
	{
		return null;
	}
}
