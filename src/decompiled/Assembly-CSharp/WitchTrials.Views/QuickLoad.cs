using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace WitchTrials.Views;

[Token(Token = "0x2000039")]
[UsedImplicitly]
public class QuickLoad : Command
{
	[StructLayout(3)]
	[Token(Token = "0x200003A")]
	[CompilerGenerated]
	private struct _003CDoQuickLoadAsync_003Ed__3
{
		[Token(Token = "0x400009D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400009E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400009F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public QuickLoad _003C_003E4__this;

		[Token(Token = "0x40000A0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private IDisposable _003C__003E5__2;

		[Token(Token = "0x40000A1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask<GameStateMap>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60000C5")]
		[Address(RVA = "0x31A370", Offset = "0x318D70", VA = "0x18031A370", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000C6")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400009B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly]
	[ParameterAlias("")]
	public IntegerParameter SlotNumber;

	[Token(Token = "0x400009C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly]
	public BooleanParameter Wait;

	[Token(Token = "0x60000C1")]
	[Address(RVA = "0x31A060", Offset = "0x318A60", VA = "0x18031A060", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000C2")]
	[Address(RVA = "0x31A110", Offset = "0x318B10", VA = "0x18031A110")]
	[AsyncStateMachine(typeof(_003CDoQuickLoadAsync_003Ed__3))]
	private UniTask DoQuickLoadAsync(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000C3")]
	[Address(RVA = "0x31A230", Offset = "0x318C30", VA = "0x18031A230")]
	private bool TryGetSlotNumber(out int slotNumber)
	{
		slotNumber = default;
		slotNumber = default;
		return default(bool);
	}

	[Token(Token = "0x60000C4")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public QuickLoad()
	{
	}
}
