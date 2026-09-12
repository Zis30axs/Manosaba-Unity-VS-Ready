using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace GigaCreation.NaninovelExtender.Ui;

[Token(Token = "0x200000C")]
public class GameStateSlotsGridExtended : ScriptableGrid<GameStateSlotExtended>
{
	[StructLayout(3)]
	[Token(Token = "0x200000E")]
	[CompilerGenerated]
	private struct _003CBindSlotAsync_003Ed__7
{
		[Token(Token = "0x4000025")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000026")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000027")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public GameStateSlotExtended slot;

		[Token(Token = "0x4000028")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public int itemIndex;

		[Token(Token = "0x4000029")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public GameStateSlotsGridExtended _003C_003E4__this;

		[Token(Token = "0x400002A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private int _003CslotNumber_003E5__2;

		[Token(Token = "0x400002B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<GameStateMap>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000036")]
		[Address(RVA = "0xD37D30", Offset = "0xD36730", VA = "0x180D37D30", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000037")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200000F")]
	[CompilerGenerated]
	private struct _003CInitializeAsync_003Ed__3
{
		[Token(Token = "0x400002C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400002D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400002E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public GameStateSlotsGridExtended _003C_003E4__this;

		[Token(Token = "0x400002F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Action<int> onSlotClicked;

		[Token(Token = "0x4000030")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public Action<int> onDeleteClicked;

		[Token(Token = "0x4000031")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public Func<int, UniTask<GameStateMap>> loadStateAt;

		[Token(Token = "0x4000032")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public int itemsCount;

		[Token(Token = "0x4000033")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000038")]
		[Address(RVA = "0xD381A0", Offset = "0xD36BA0", VA = "0x180D381A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000039")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000021")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
	private Action<int> _onSlotClicked;

	[Token(Token = "0x4000022")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
	private Action<int> _onDeleteClicked;

	[Token(Token = "0x4000023")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
	private Func<int, UniTask<GameStateMap>> _loadStateAt;

	[Token(Token = "0x600002E")]
	[Address(RVA = "0xD37560", Offset = "0xD35F60", VA = "0x180D37560")]
	[AsyncStateMachine(typeof(_003CInitializeAsync_003Ed__3))]
	public UniTask InitializeAsync(int itemsCount, Action<int> onSlotClicked, Action<int> onDeleteClicked, Func<int, UniTask<GameStateMap>> loadStateAt)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600002F")]
	[Address(RVA = "0x631470", Offset = "0x62FE70", VA = "0x180631470", Slot = "73")]
	protected override void InitializeSlot(GameStateSlotExtended slot)
	{
	}

	[Token(Token = "0x6000030")]
	[Address(RVA = "0xD377B0", Offset = "0xD361B0", VA = "0x180D377B0")]
	public void BindSlot(int slotNumber, GameStateMap state)
	{
	}

	[Token(Token = "0x6000031")]
	[Address(RVA = "0xD37970", Offset = "0xD36370", VA = "0x180D37970", Slot = "74")]
	protected override void BindSlot(GameStateSlotExtended slot, int itemIndex)
	{
	}

	[Token(Token = "0x6000032")]
	[Address(RVA = "0xD37B20", Offset = "0xD36520", VA = "0x180D37B20")]
	[AsyncStateMachine(typeof(_003CBindSlotAsync_003Ed__7))]
	private UniTask BindSlotAsync(GameStateSlotExtended slot, int itemIndex)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000033")]
	[Address(RVA = "0xD37CC0", Offset = "0xD366C0", VA = "0x180D37CC0")]
	public GameStateSlotsGridExtended()
	{
	}
}
