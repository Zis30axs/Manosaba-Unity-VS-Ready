using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace WitchTrials.Views;

[Token(Token = "0x200003B")]
[UsedImplicitly]
public class QuickSave : Command
{
	[StructLayout(3)]
	[Token(Token = "0x200003C")]
	[CompilerGenerated]
	private struct _003CDoQuickSaveAsync_003Ed__4
{
		[Token(Token = "0x40000A5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000A6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40000A7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public QuickSave _003C_003E4__this;

		[Token(Token = "0x40000A8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private IDisposable _003C__003E5__2;

		[Token(Token = "0x40000A9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask<GameStateMap>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60000CA")]
		[Address(RVA = "0x31AC30", Offset = "0x319630", VA = "0x18031AC30", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000CB")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40000A2")]
	public const string LastQuickSaveSlotNumberVarName = "lastQuickSaveSlotNumber";

	[Token(Token = "0x40000A3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly]
	[RequiredParameter]
	[ParameterAlias("")]
	public IntegerParameter SlotNumber;

	[Token(Token = "0x40000A4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly]
	public BooleanParameter Wait;

	[Token(Token = "0x60000C7")]
	[Address(RVA = "0x31AA60", Offset = "0x319460", VA = "0x18031AA60", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000C8")]
	[Address(RVA = "0x31AB10", Offset = "0x319510", VA = "0x18031AB10")]
	[AsyncStateMachine(typeof(_003CDoQuickSaveAsync_003Ed__4))]
	private UniTask DoQuickSaveAsync(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000C9")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public QuickSave()
	{
	}
}
