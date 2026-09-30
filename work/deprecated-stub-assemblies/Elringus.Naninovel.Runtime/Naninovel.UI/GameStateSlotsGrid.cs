using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.UI
{
    [Token(Token = "0x200056C")]
    public class GameStateSlotsGrid : ScriptableGrid<GameStateSlot>
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200056E")]
    	[CompilerGenerated]
    	private struct _003CBindSlot_003Ed__7
    {
    		[Token(Token = "0x40014AF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40014B0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncVoidMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40014B1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public int itemIndex;

    		[Token(Token = "0x40014B2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public GameStateSlotsGrid _003C_003E4__this;

    		[Token(Token = "0x40014B3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public GameStateSlot slot;

    		[Token(Token = "0x40014B4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private int _003CslotNumber_003E5__2;

    		[Token(Token = "0x40014B5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask<GameStateMap>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001AF5")]
    		[Address(RVA = "0x631710", Offset = "0x630110", VA = "0x180631710", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001AF6")]
    		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200056F")]
    	[CompilerGenerated]
    	private struct _003CInitialize_003Ed__3
    {
    		[Token(Token = "0x40014B6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40014B7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40014B8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GameStateSlotsGrid _003C_003E4__this;

    		[Token(Token = "0x40014B9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public Action<int> onSlotClicked;

    		[Token(Token = "0x40014BA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public Action<int> onDeleteClicked;

    		[Token(Token = "0x40014BB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public Func<int, UniTask<GameStateMap>> loadStateAt;

    		[Token(Token = "0x40014BC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public int itemsCount;

    		[Token(Token = "0x40014BD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001AF7")]
    		[Address(RVA = "0x631A70", Offset = "0x630470", VA = "0x180631A70", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001AF8")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40014AB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
    	private Action<int> onSlotClicked;

    	[Token(Token = "0x40014AC")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
    	private Action<int> onDeleteClicked;

    	[Token(Token = "0x40014AD")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
    	private Func<int, UniTask<GameStateMap>> loadStateAt;

    	[Token(Token = "0x6001AED")]
    	[Address(RVA = "0x6310B0", Offset = "0x62FAB0", VA = "0x1806310B0")]
    	[AsyncStateMachine(typeof(_003CInitialize_003Ed__3))]
    	public UniTask Initialize(int itemsCount, Action<int> onSlotClicked, Action<int> onDeleteClicked, Func<int, UniTask<GameStateMap>> loadStateAt)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001AEE")]
    	[Address(RVA = "0x631300", Offset = "0x62FD00", VA = "0x180631300", Slot = "76")]
    	public virtual void BindSlot(int slotNumber, GameStateMap state)
    	{
    	}

    	[Token(Token = "0x6001AEF")]
    	[Address(RVA = "0x631420", Offset = "0x62FE20", VA = "0x180631420")]
    	protected new UniTask Initialize(int itemsCount)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001AF0")]
    	[Address(RVA = "0x631470", Offset = "0x62FE70", VA = "0x180631470", Slot = "73")]
    	protected override void InitializeSlot(GameStateSlot slot)
    	{
    	}

    	[Token(Token = "0x6001AF1")]
    	[Address(RVA = "0x6314B0", Offset = "0x62FEB0", VA = "0x1806314B0", Slot = "74")]
    	[AsyncStateMachine(typeof(_003CBindSlot_003Ed__7))]
    	protected override void BindSlot(GameStateSlot slot, int itemIndex)
    	{
    	}

    	[Token(Token = "0x6001AF2")]
    	[Address(RVA = "0x631660", Offset = "0x630060", VA = "0x180631660")]
    	public GameStateSlotsGrid()
    	{
    	}
    }
}
