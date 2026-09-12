using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI;

[Token(Token = "0x2000571")]
[RequireComponent(typeof(CanvasGroup))]
public class SaveLoadMenu : CustomUI, ISaveLoadUI, IManagedUI
{
	[Serializable]
	[Token(Token = "0x2000572")]
	private class GlobalState
	{
		[Token(Token = "0x40014CC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public bool LastSaveWasQuick;

		[Token(Token = "0x6001B20")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public GlobalState()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000573")]
	[CompilerGenerated]
	private struct _003CHandleDeleteQuickLoadSlotClicked_003Ed__47
{
		[Token(Token = "0x40014CD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40014CE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40014CF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public SaveLoadMenu _003C_003E4__this;

		[Token(Token = "0x40014D0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public int slotNumber;

		[Token(Token = "0x40014D1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private string _003CslotId_003E5__2;

		[Token(Token = "0x40014D2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask<bool>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001B21")]
		[Address(RVA = "0x634100", Offset = "0x632B00", VA = "0x180634100", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001B22")]
		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000574")]
	[CompilerGenerated]
	private struct _003CHandleDeleteSlotClicked_003Ed__46
{
		[Token(Token = "0x40014D3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40014D4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40014D5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public SaveLoadMenu _003C_003E4__this;

		[Token(Token = "0x40014D6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public int slotNumber;

		[Token(Token = "0x40014D7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private string _003CslotId_003E5__2;

		[Token(Token = "0x40014D8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask<bool>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001B23")]
		[Address(RVA = "0x6345D0", Offset = "0x632FD0", VA = "0x1806345D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001B24")]
		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000575")]
	[CompilerGenerated]
	private struct _003CHandleGameSaveFinished_003Ed__49
{
		[Token(Token = "0x40014D9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40014DA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40014DB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public GameSaveLoadArgs args;

		[Token(Token = "0x40014DC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public SaveLoadMenu _003C_003E4__this;

		[Token(Token = "0x40014DD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask<GameStateMap>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001B25")]
		[Address(RVA = "0x634AE0", Offset = "0x6334E0", VA = "0x180634AE0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001B26")]
		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000576")]
	[CompilerGenerated]
	private struct _003CHandleLoadSlotClicked_003Ed__42
{
		[Token(Token = "0x40014DE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40014DF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40014E0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public SaveLoadMenu _003C_003E4__this;

		[Token(Token = "0x40014E1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string slotId;

		[Token(Token = "0x40014E2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40014E3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private IDisposable _003C_003E7__wrap1;

		[Token(Token = "0x40014E4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask<IDisposable>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x40014E5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private UniTask<GameStateMap>.Awaiter _003C_003Eu__3;

		[Token(Token = "0x6001B27")]
		[Address(RVA = "0x6350B0", Offset = "0x633AB0", VA = "0x1806350B0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001B28")]
		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000577")]
	[CompilerGenerated]
	private struct _003CHandleSaveSlotClicked_003Ed__45
{
		[Token(Token = "0x40014E6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40014E7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40014E8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public SaveLoadMenu _003C_003E4__this;

		[Token(Token = "0x40014E9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string slotId;

		[Token(Token = "0x40014EA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public int slotNumber;

		[Token(Token = "0x40014EB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask<bool>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40014EC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private InteractionBlocker _003C_003E7__wrap1;

		[Token(Token = "0x40014ED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private UniTask<GameStateMap>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6001B29")]
		[Address(RVA = "0x635A90", Offset = "0x634490", VA = "0x180635A90", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001B2A")]
		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000578")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__35
{
		[Token(Token = "0x40014EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40014EF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40014F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SaveLoadMenu _003C_003E4__this;

		[Token(Token = "0x40014F1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001B2B")]
		[Address(RVA = "0x6362B0", Offset = "0x634CB0", VA = "0x1806362B0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001B2C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000579")]
	[CompilerGenerated]
	private struct _003CLoadQuickSaveSlot_003Ed__51
{
		[Token(Token = "0x40014F2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40014F3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<GameStateMap> _003C_003Et__builder;

		[Token(Token = "0x40014F4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public SaveLoadMenu _003C_003E4__this;

		[Token(Token = "0x40014F5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public int slotNumber;

		[Token(Token = "0x40014F6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<GameStateMap>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001B2D")]
		[Address(RVA = "0x636F60", Offset = "0x635960", VA = "0x180636F60", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001B2E")]
		[Address(RVA = "0x637290", Offset = "0x635C90", VA = "0x180637290", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200057A")]
	[CompilerGenerated]
	private struct _003CLoadSaveSlot_003Ed__50
{
		[Token(Token = "0x40014F7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40014F8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<GameStateMap> _003C_003Et__builder;

		[Token(Token = "0x40014F9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public SaveLoadMenu _003C_003E4__this;

		[Token(Token = "0x40014FA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public int slotNumber;

		[Token(Token = "0x40014FB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<GameStateMap>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001B2F")]
		[Address(RVA = "0x6372C0", Offset = "0x635CC0", VA = "0x1806372C0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001B30")]
		[Address(RVA = "0x6375F0", Offset = "0x635FF0", VA = "0x1806375F0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200057B")]
	[CompilerGenerated]
	private struct _003CPlayTitleLoad_003Ed__52
{
		[Token(Token = "0x40014FC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40014FD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40014FE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SaveLoadMenu _003C_003E4__this;

		[Token(Token = "0x40014FF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private string _003CscriptPath_003E5__2;

		[Token(Token = "0x4001500")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask<Resource<Script>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4001501")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6001B31")]
		[Address(RVA = "0x637620", Offset = "0x636020", VA = "0x180637620", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001B32")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40014BE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[ManagedText("DefaultUI")]
	protected static string OverwriteSaveSlotMessage;

	[Token(Token = "0x40014BF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
	[ManagedText("DefaultUI")]
	protected static string DeleteSaveSlotMessage;

	[Token(Token = "0x40014C0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	[Header("Tabs")]
	[SerializeField]
	private Toggle quickLoadToggle;

	[Token(Token = "0x40014C1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	[SerializeField]
	private Toggle saveToggle;

	[Token(Token = "0x40014C2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
	[SerializeField]
	private Toggle loadToggle;

	[Token(Token = "0x40014C3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
	[Header("Grids")]
	[SerializeField]
	private GameStateSlotsGrid quickLoadGrid;

	[Token(Token = "0x40014C4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
	[SerializeField]
	private GameStateSlotsGrid saveGrid;

	[Token(Token = "0x40014C5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x108")]
	[SerializeField]
	private GameStateSlotsGrid loadGrid;

	[Token(Token = "0x40014C6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x110")]
	private IStateManager stateManager;

	[Token(Token = "0x40014C7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x118")]
	private IScriptPlayer scriptPlayer;

	[Token(Token = "0x40014C8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x120")]
	private IScriptManager scripts;

	[Token(Token = "0x40014C9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x128")]
	private IConfirmationUI confirmationUI;

	[Token(Token = "0x40014CA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x130")]
	private SaveLoadUIPresentationMode presentationMode;

	[Token(Token = "0x40014CB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x138")]
	private Transform tabLegend;

	[Token(Token = "0x170003D3")]
	public virtual SaveLoadUIPresentationMode PresentationMode
	{
		[Token(Token = "0x6001AFC")]
		[Address(RVA = "0x631E70", Offset = "0x630870", VA = "0x180631E70", Slot = "98")]
		get
		{
			return default(SaveLoadUIPresentationMode);
		}
		[Token(Token = "0x6001AFD")]
		[Address(RVA = "0x631E80", Offset = "0x630880", VA = "0x180631E80", Slot = "99")]
		set
		{
		}
	}

	[Token(Token = "0x170003D4")]
	protected virtual bool LastSaveWasQuick
	{
		[Token(Token = "0x6001AFE")]
		[Address(RVA = "0x631EA0", Offset = "0x6308A0", VA = "0x180631EA0", Slot = "100")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6001AFF")]
		[Address(RVA = "0x631F20", Offset = "0x630920", VA = "0x180631F20", Slot = "101")]
		set
		{
		}
	}

	[Token(Token = "0x170003D5")]
	protected virtual Toggle QuickLoadToggle
	{
		[Token(Token = "0x6001B00")]
		[Address(RVA = "0x33E0B0", Offset = "0x33CAB0", VA = "0x18033E0B0", Slot = "102")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003D6")]
	protected virtual Toggle SaveToggle
	{
		[Token(Token = "0x6001B01")]
		[Address(RVA = "0x49D2A0", Offset = "0x49BCA0", VA = "0x18049D2A0", Slot = "103")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003D7")]
	protected virtual Toggle LoadToggle
	{
		[Token(Token = "0x6001B02")]
		[Address(RVA = "0x49D2B0", Offset = "0x49BCB0", VA = "0x18049D2B0", Slot = "104")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003D8")]
	protected virtual GameStateSlotsGrid QuickLoadGrid
	{
		[Token(Token = "0x6001B03")]
		[Address(RVA = "0x49D250", Offset = "0x49BC50", VA = "0x18049D250", Slot = "105")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003D9")]
	protected virtual GameStateSlotsGrid SaveGrid
	{
		[Token(Token = "0x6001B04")]
		[Address(RVA = "0x37A040", Offset = "0x378A40", VA = "0x18037A040", Slot = "106")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003DA")]
	protected virtual GameStateSlotsGrid LoadGrid
	{
		[Token(Token = "0x6001B05")]
		[Address(RVA = "0x37A0B0", Offset = "0x378AB0", VA = "0x18037A0B0", Slot = "107")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003DB")]
	private ISaveSlotManager<GameStateMap> slotManager
	{
		[Token(Token = "0x6001B06")]
		[Address(RVA = "0x632040", Offset = "0x630A40", VA = "0x180632040")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6001B07")]
	[Address(RVA = "0x6320A0", Offset = "0x630AA0", VA = "0x1806320A0", Slot = "83")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__35))]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001B08")]
	[Address(RVA = "0x6321C0", Offset = "0x630BC0", VA = "0x1806321C0", Slot = "108")]
	public virtual SaveLoadUIPresentationMode GetLastLoadMode()
	{
		return default(SaveLoadUIPresentationMode);
	}

	[Token(Token = "0x6001B09")]
	[Address(RVA = "0x6321F0", Offset = "0x630BF0", VA = "0x1806321F0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001B0A")]
	[Address(RVA = "0x632470", Offset = "0x630E70", VA = "0x180632470", Slot = "8")]
	protected override void OnDestroy()
	{
	}

	[Token(Token = "0x6001B0B")]
	[Address(RVA = "0x632570", Offset = "0x630F70", VA = "0x180632570", Slot = "109")]
	protected virtual void SetPresentationMode(SaveLoadUIPresentationMode value)
	{
	}

	[Token(Token = "0x6001B0C")]
	[Address(RVA = "0x632900", Offset = "0x631300", VA = "0x180632900", Slot = "110")]
	protected virtual void HandleLoadSlotClicked(int slotNumber)
	{
	}

	[Token(Token = "0x6001B0D")]
	[Address(RVA = "0x632980", Offset = "0x631380", VA = "0x180632980", Slot = "111")]
	protected virtual void HandleQuickLoadSlotClicked(int slotNumber)
	{
	}

	[Token(Token = "0x6001B0E")]
	[Address(RVA = "0x632A00", Offset = "0x631400", VA = "0x180632A00", Slot = "112")]
	[AsyncStateMachine(typeof(_003CHandleLoadSlotClicked_003Ed__42))]
	protected virtual void HandleLoadSlotClicked(string slotId)
	{
	}

	[Token(Token = "0x6001B0F")]
	[Address(RVA = "0x632BB0", Offset = "0x6315B0", VA = "0x180632BB0", Slot = "113")]
	protected virtual void HandleSaveSlotClicked(int slotNumber)
	{
	}

	[Token(Token = "0x6001B10")]
	[Address(RVA = "0x632C30", Offset = "0x631630", VA = "0x180632C30", Slot = "114")]
	protected virtual void HandleQuickSaveSlotClicked(int slotNumber)
	{
	}

	[Token(Token = "0x6001B11")]
	[Address(RVA = "0x632CB0", Offset = "0x6316B0", VA = "0x180632CB0", Slot = "115")]
	[AsyncStateMachine(typeof(_003CHandleSaveSlotClicked_003Ed__45))]
	protected virtual void HandleSaveSlotClicked(string slotId, int slotNumber)
	{
	}

	[Token(Token = "0x6001B12")]
	[Address(RVA = "0x632E60", Offset = "0x631860", VA = "0x180632E60", Slot = "116")]
	[AsyncStateMachine(typeof(_003CHandleDeleteSlotClicked_003Ed__46))]
	protected virtual void HandleDeleteSlotClicked(int slotNumber)
	{
	}

	[Token(Token = "0x6001B13")]
	[Address(RVA = "0x632FB0", Offset = "0x6319B0", VA = "0x180632FB0", Slot = "117")]
	[AsyncStateMachine(typeof(_003CHandleDeleteQuickLoadSlotClicked_003Ed__47))]
	protected virtual void HandleDeleteQuickLoadSlotClicked(int slotNumber)
	{
	}

	[Token(Token = "0x6001B14")]
	[Address(RVA = "0x633100", Offset = "0x631B00", VA = "0x180633100", Slot = "118")]
	protected virtual void HandleGameSaveStarted(GameSaveLoadArgs args)
	{
	}

	[Token(Token = "0x6001B15")]
	[Address(RVA = "0x633120", Offset = "0x631B20", VA = "0x180633120", Slot = "119")]
	[AsyncStateMachine(typeof(_003CHandleGameSaveFinished_003Ed__49))]
	protected virtual void HandleGameSaveFinished(GameSaveLoadArgs args)
	{
	}

	[Token(Token = "0x6001B16")]
	[Address(RVA = "0x6332C0", Offset = "0x631CC0", VA = "0x1806332C0", Slot = "120")]
	[AsyncStateMachine(typeof(_003CLoadSaveSlot_003Ed__50))]
	protected virtual UniTask<GameStateMap> LoadSaveSlot(int slotNumber)
	{
		return default(UniTask<GameStateMap>);
	}

	[Token(Token = "0x6001B17")]
	[Address(RVA = "0x633470", Offset = "0x631E70", VA = "0x180633470", Slot = "121")]
	[AsyncStateMachine(typeof(_003CLoadQuickSaveSlot_003Ed__51))]
	protected virtual UniTask<GameStateMap> LoadQuickSaveSlot(int slotNumber)
	{
		return default(UniTask<GameStateMap>);
	}

	[Token(Token = "0x6001B18")]
	[Address(RVA = "0x633620", Offset = "0x632020", VA = "0x180633620", Slot = "122")]
	[AsyncStateMachine(typeof(_003CPlayTitleLoad_003Ed__52))]
	protected virtual UniTask PlayTitleLoad()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001B19")]
	[Address(RVA = "0x633750", Offset = "0x632150", VA = "0x180633750", Slot = "123")]
	protected virtual void HandlePageInput(float value)
	{
	}

	[Token(Token = "0x6001B1A")]
	[Address(RVA = "0x633850", Offset = "0x632250", VA = "0x180633850", Slot = "124")]
	protected virtual void HandleTabInput(float value)
	{
	}

	[Token(Token = "0x6001B1B")]
	[Address(RVA = "0x633960", Offset = "0x632360", VA = "0x180633960", Slot = "125")]
	protected virtual GameStateSlotsGrid GetActiveGrid()
	{
		return null;
	}

	[Token(Token = "0x6001B1C")]
	[Address(RVA = "0x633A00", Offset = "0x632400", VA = "0x180633A00", Slot = "92")]
	protected override GameObject FindFocusObject()
	{
		return null;
	}

	[Token(Token = "0x6001B1D")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	public SaveLoadMenu()
	{
	}
}
