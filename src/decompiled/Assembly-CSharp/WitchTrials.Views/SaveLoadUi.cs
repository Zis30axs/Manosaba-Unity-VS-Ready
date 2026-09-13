using System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using GigaCreation.Essentials.Dialogs.Models;
using GigaCreation.Essentials.Dialogs.Views;
using GigaCreation.Essentials.Input;
using GigaCreation.Essentials.Overlays.Models;
using GigaCreation.Essentials.Ui;
using GigaCreation.NaninovelExtender.Common;
using GigaCreation.NaninovelExtender.Ui;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using UniRx;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x20000D1")]
[RequireComponent(typeof(CanvasGroup))]
public class SaveLoadUi : CustomUI, ISaveLoadUI, IManagedUI
{
	[Serializable]
	[Token(Token = "0x20000D2")]
	public class SaveLoadPanelReference
	{
		[Token(Token = "0x40003D0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		[SerializeField]
		private SaveLoadUIPresentationMode _mode;

		[Token(Token = "0x40003D1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		[SerializeField]
		private SaveLoadPanel _panel;

		[Token(Token = "0x17000068")]
		public SaveLoadUIPresentationMode Mode
		{
			[Token(Token = "0x6000328")]
			[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
			get
			{
				return default(SaveLoadUIPresentationMode);
			}
		}

		[Token(Token = "0x17000069")]
		public SaveLoadPanel Panel
		{
			[Token(Token = "0x6000329")]
			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x600032A")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public SaveLoadPanelReference()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000D4")]
	[CompilerGenerated]
	private struct _003CHandleDeleteSlotClickedAsync_003Ed__50
{
		[Token(Token = "0x40003D5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40003D6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40003D7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SaveLoadUi _003C_003E4__this;

		[Token(Token = "0x40003D8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public int slotNumber;

		[Token(Token = "0x40003D9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x40003DA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private string _003CslotId_003E5__2;

		[Token(Token = "0x40003DB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private INaninovelInteractionBlocker _003Cblocker_003E5__3;

		[Token(Token = "0x40003DC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private ProvidableDialog _003Cdialog_003E5__4;

		[Token(Token = "0x40003DD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private Cysharp.Threading.Tasks.UniTask<ResponseKind>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600032F")]
		[Address(RVA = "0x35B8F0", Offset = "0x35A2F0", VA = "0x18035B8F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000330")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000D5")]
	[CompilerGenerated]
	private struct _003CHandleLoadSlotClickedAsync_003Ed__48
{
		[Token(Token = "0x40003DE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40003DF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40003E0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SaveLoadUi _003C_003E4__this;

		[Token(Token = "0x40003E1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public int slotNumber;

		[Token(Token = "0x40003E2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x40003E3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private string _003CslotId_003E5__2;

		[Token(Token = "0x40003E4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private NaninovelInteractionBlocker _003C__003E5__3;

		[Token(Token = "0x40003E5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40003E6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x40003E7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private Naninovel.UniTask<GameStateMap>.Awaiter _003C_003Eu__3;

		[Token(Token = "0x6000331")]
		[Address(RVA = "0x35C490", Offset = "0x35AE90", VA = "0x18035C490", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000332")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000D6")]
	[CompilerGenerated]
	private struct _003CHandleSaveSlotClickedAsync_003Ed__46
{
		[Token(Token = "0x40003E8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40003E9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40003EA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SaveLoadUi _003C_003E4__this;

		[Token(Token = "0x40003EB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public int slotNumber;

		[Token(Token = "0x40003EC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x40003ED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private string _003CslotId_003E5__2;

		[Token(Token = "0x40003EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private INaninovelInteractionBlocker _003Cblocker_003E5__3;

		[Token(Token = "0x40003EF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private CancellationTokenSource _003ClinkedCts_003E5__4;

		[Token(Token = "0x40003F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private IDisposable _003C__003E5__5;

		[Token(Token = "0x40003F1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private IDisposable _003C___003E5__6;

		[Token(Token = "0x40003F2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private Naninovel.UniTask<GameStateMap> _003CsaveGameTask_003E5__7;

		[Token(Token = "0x40003F3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
		private ProvidableDialog _003Cdialog_003E5__8;

		[Token(Token = "0x40003F4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
		private Cysharp.Threading.Tasks.UniTask<ResponseKind>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40003F5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
		private Naninovel.YieldAwaitable.Awaiter _003C_003Eu__2;

		[Token(Token = "0x40003F6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
		private Naninovel.UniTask<GameStateMap>.Awaiter _003C_003Eu__3;

		[Token(Token = "0x6000333")]
		[Address(RVA = "0x35D830", Offset = "0x35C230", VA = "0x18035D830", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000334")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000D7")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__42
{
		[Token(Token = "0x40003F7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40003F8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40003F9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SaveLoadUi _003C_003E4__this;

		[Token(Token = "0x40003FA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000335")]
		[Address(RVA = "0x35EFC0", Offset = "0x35D9C0", VA = "0x18035EFC0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000336")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000D8")]
	[CompilerGenerated]
	private struct _003CLoadSaveSlotAsync_003Ed__51
{
		[Token(Token = "0x40003FB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40003FC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<GameStateMap> _003C_003Et__builder;

		[Token(Token = "0x40003FD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public SaveLoadUi _003C_003E4__this;

		[Token(Token = "0x40003FE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public int slotNumber;

		[Token(Token = "0x40003FF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Naninovel.UniTask<GameStateMap>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000337")]
		[Address(RVA = "0x35F870", Offset = "0x35E270", VA = "0x18035F870", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000338")]
		[Address(RVA = "0x35FE90", Offset = "0x35E890", VA = "0x18035FE90", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40003BA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	[Header("References")]
	[SerializeField]
	private NaninovelScreen _naninovelScreen;

	[Token(Token = "0x40003BB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	[SerializeField]
	private SaveLoadPanelReference[] _panelReferences;

	[Token(Token = "0x40003BC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
	[Header("Parameters")]
	[SerializeField]
	private float _fadeOutDurationOnLoad;

	[Token(Token = "0x40003BD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF4")]
	[SerializeField]
	private float _fadeInDurationOnLoad;

	[Token(Token = "0x40003BE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
	[SerializeField]
	private Color _fadeColorOnLoad;

	[Token(Token = "0x40003BF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x108")]
	[SerializeField]
	private float _saveGameTimeout;

	[Token(Token = "0x40003C0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10C")]
	[SerializeField]
	private float _loadDelay;

	[Token(Token = "0x40003C1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x110")]
	[Space]
	[ResourcePopup("Audio")]
	[SerializeField]
	private string _saveSfxPath;

	[Token(Token = "0x40003C2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x118")]
	[ResourcePopup("Audio")]
	[SerializeField]
	private string _loadSfxPath;

	[Token(Token = "0x40003C3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x120")]
	[Space]
	[SerializeField]
	private DialogData _overwriteSaveSlotDialogData;

	[Token(Token = "0x40003C4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x128")]
	[SerializeField]
	private DialogData _deleteSaveSlotDialogData;

	[Token(Token = "0x40003C5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x130")]
	private GigaCreation.Essentials.Input.IInputManager _inputManager;

	[Token(Token = "0x40003C6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x138")]
	private IOverlayFader _overlayFader;

	[Token(Token = "0x40003C7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x140")]
	private IDialogProvider _dialogProvider;

	[Token(Token = "0x40003C8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x148")]
	private IWitchTrialsNaninovelManager _witchTrialsNaninovelManager;

	[Token(Token = "0x40003C9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x150")]
	private IStateManager _naniStateManager;

	[Token(Token = "0x40003CA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x158")]
	private IAudioManager _naniAudioManager;

	[Token(Token = "0x40003CB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x160")]
	private IUIManager _naniUiManager;

	[Token(Token = "0x40003CC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x168")]
	private SaveLoadUIPresentationMode _presentationMode;

	[Token(Token = "0x40003CD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x170")]
	private Dictionary<SaveLoadUIPresentationMode, SaveLoadPanel> _panels;

	[Token(Token = "0x40003CE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x178")]
	private ReactiveProperty<int> _loadingSlotCount;

	[Token(Token = "0x40003CF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x180")]
	private IDisposable _loadingScreenOnPaginate;

	[Token(Token = "0x1700005F")]
	public SaveLoadUIPresentationMode PresentationMode
	{
		[Token(Token = "0x600030C")]
		[Address(RVA = "0x359350", Offset = "0x357D50", VA = "0x180359350", Slot = "95")]
		get
		{
			return default(SaveLoadUIPresentationMode);
		}
		[Token(Token = "0x600030D")]
		[Address(RVA = "0x359360", Offset = "0x357D60", VA = "0x180359360", Slot = "96")]
		set
		{
		}
	}

	[Token(Token = "0x17000060")]
	private GigaCreation.Essentials.Input.IInputManager InputManager
	{
		[Token(Token = "0x600030E")]
		[Address(RVA = "0x3594A0", Offset = "0x357EA0", VA = "0x1803594A0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000061")]
	private IOverlayFader OverlayFader
	{
		[Token(Token = "0x600030F")]
		[Address(RVA = "0x359570", Offset = "0x357F70", VA = "0x180359570")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000062")]
	private IDialogProvider DialogProvider
	{
		[Token(Token = "0x6000310")]
		[Address(RVA = "0x359640", Offset = "0x358040", VA = "0x180359640")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000063")]
	private IWitchTrialsNaninovelManager WitchTrialsNaninovelManager
	{
		[Token(Token = "0x6000311")]
		[Address(RVA = "0x359710", Offset = "0x358110", VA = "0x180359710")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000064")]
	private IStateManager NaniStateManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x3597E0
		// Lazily-cached engine service lookup, same pattern as
		// StageScreenController.TrialStateManager: `_naniStateManager ??=
		// Naninovel.Engine.GetServiceOrErr<IStateManager>();`
		[Token(Token = "0x6000312")]
		[Address(RVA = "0x3597E0", Offset = "0x3581E0", VA = "0x1803597E0")]
		get
		{
			return _naniStateManager ??= Engine.GetServiceOrErr<IStateManager>();
		}
	}

	[Token(Token = "0x17000065")]
	private IAudioManager NaniAudioManager
	{
		[Token(Token = "0x6000313")]
		[Address(RVA = "0x3598B0", Offset = "0x3582B0", VA = "0x1803598B0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000066")]
	private IUIManager NaniUiManager
	{
		[Token(Token = "0x6000314")]
		[Address(RVA = "0x359980", Offset = "0x358380", VA = "0x180359980")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000067")]
	private ISaveSlotManager<GameStateMap> SlotManager
	{
		[Token(Token = "0x6000315")]
		[Address(RVA = "0x359A50", Offset = "0x358450", VA = "0x180359A50")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000316")]
	[Address(RVA = "0x359AB0", Offset = "0x3584B0", VA = "0x180359AB0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6000317")]
	[Address(RVA = "0x359EF0", Offset = "0x3588F0", VA = "0x180359EF0", Slot = "83")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__42))]
	public override Naninovel.UniTask Initialize()
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000318")]
	[Address(RVA = "0x359350", Offset = "0x357D50", VA = "0x180359350", Slot = "97")]
	public SaveLoadUIPresentationMode GetLastLoadMode()
	{
		return default(SaveLoadUIPresentationMode);
	}

	[Token(Token = "0x6000319")]
	[Address(RVA = "0x359360", Offset = "0x357D60", VA = "0x180359360")]
	private void SetPresentationMode(SaveLoadUIPresentationMode value)
	{
	}

	[Token(Token = "0x600031A")]
	[Address(RVA = "0x35A010", Offset = "0x358A10", VA = "0x18035A010")]
	private void HandleSaveSlotClicked(int slotNumber)
	{
	}

	[Token(Token = "0x600031B")]
	[Address(RVA = "0x35A1C0", Offset = "0x358BC0", VA = "0x18035A1C0")]
	[AsyncStateMachine(typeof(_003CHandleSaveSlotClickedAsync_003Ed__46))]
	private Naninovel.UniTask HandleSaveSlotClickedAsync(int slotNumber, [Optional] CancellationToken ct)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x600031C")]
	[Address(RVA = "0x35A350", Offset = "0x358D50", VA = "0x18035A350")]
	private void HandleLoadSlotClicked(int slotNumber)
	{
	}

	[Token(Token = "0x600031D")]
	[Address(RVA = "0x35A510", Offset = "0x358F10", VA = "0x18035A510")]
	[AsyncStateMachine(typeof(_003CHandleLoadSlotClickedAsync_003Ed__48))]
	private Naninovel.UniTask HandleLoadSlotClickedAsync(int slotNumber, [Optional] CancellationToken ct)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x600031E")]
	[Address(RVA = "0x35A6B0", Offset = "0x3590B0", VA = "0x18035A6B0")]
	private void HandleDeleteSlotClicked(int slotNumber)
	{
	}

	[Token(Token = "0x600031F")]
	[Address(RVA = "0x35A860", Offset = "0x359260", VA = "0x18035A860")]
	[AsyncStateMachine(typeof(_003CHandleDeleteSlotClickedAsync_003Ed__50))]
	private Naninovel.UniTask HandleDeleteSlotClickedAsync(int slotNumber, [Optional] CancellationToken ct)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000320")]
	[Address(RVA = "0x35AA00", Offset = "0x359400", VA = "0x18035AA00")]
	[AsyncStateMachine(typeof(_003CLoadSaveSlotAsync_003Ed__51))]
	private Naninovel.UniTask<GameStateMap> LoadSaveSlotAsync(int slotNumber)
	{
		return default(Naninovel.UniTask<GameStateMap>);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x35ABB0
	//
	// Native evidence:
	//   0x35AC6F  _loadingSlotCount?.Dispose()   (skip-on-null: real `?.`, code
	//             continues normally either way -- not the throw-on-null shape
	//             seen for the two checks below)
	//   0x35AC87  _loadingSlotCount = new ReactiveProperty<int>(0)
	//   0x35AD01..0x35AD50  _loadingSlotCount.Subscribe(<OnShow>b__52_0).AddTo(this)
	//   0x35AD55  _panels[_presentationMode].Present()   (both null checks here
	//             land on the shared NullReferenceException throw, i.e. IL2CPP's
	//             standard implicit-null-check-before-dispatch boilerplate for a
	//             plain indexer + interface call -- not a `?.` operator)
	[Token(Token = "0x6000321")]
	[Address(RVA = "0x35ABB0", Offset = "0x3595B0", VA = "0x18035ABB0")]
	public void OnShow()
	{
		_loadingSlotCount?.Dispose();
		_loadingSlotCount = new ReactiveProperty<int>(0);
		_loadingSlotCount.Subscribe(OnShowSlotCountChanged).AddTo(this);
		_panels[_presentationMode].Present();
	}

	// PARTIALLY RECONSTRUCTED | Confidence: D (behavioral summary only, body not
	// verified) | RVA: 0x35B6C0
	//
	// This method (`<OnShow>b__52_0` in dump.cs -- a compiler-generated lambda
	// backing method) is entirely ABSENT from the decompiled skeleton, even
	// though OnShow() genuinely subscribes to it (see above). This is a
	// DummyDll-generation gap, not a deliberate omission -- added here only
	// because OnShow's own reconstruction requires a real callable target.
	// Renamed from the compiler's `<OnShow>b__52_0` to a readable name since the
	// original can't be spelled as a C# identifier anyway.
	//
	// Native evidence gathered but NOT fully traced: branches on the `cnt`
	// parameter and `_loadingScreenOnPaginate`, calls
	// `GigaCreation.NaninovelExtender.Common.NaninovelLoadingScreen.Show(...)`
	// storing the result back into `_loadingScreenOnPaginate`, and elsewhere
	// disposes and clears that same field. Reads as "show a loading indicator
	// while a page of save slots is loading, hide it once done" but the exact
	// condition on `cnt` (loading-count reaching zero vs. some other check) is
	// UNVERIFIED -- do not treat the summary as the implementation.
	private void OnShowSlotCountChanged(int cnt)
	{
		throw new NotImplementedException();
	}

	[Token(Token = "0x6000322")]
	[Address(RVA = "0x35ADB0", Offset = "0x3597B0", VA = "0x18035ADB0")]
	public void OnHide()
	{
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x35AE50
	//
	// Iterates `_panels.Values` and calls `.Dismiss()` on each. The
	// disassembly does not show a plain `call` to
	// PageableGridPanel<GameStateSlotsGridExtended, GameStateSlotExtended>.Dismiss()
	// -- instead it shows what looks like that method's own field manipulation
	// (CanvasGroup.set_alpha/interactable/blocksRaycasts, Pagination.Dismiss(),
	// disposing a subscription) sitting directly inside this loop. That is
	// Dismiss()'s fully-shared-generic body appearing inline at the call site,
	// not logic written by hand in OnEndToDismiss itself -- the MethodInfo slot
	// initialized at the top of this function names that exact method. Reproducing
	// those field pokes here would misattribute PageableGridPanel's own
	// implementation as SaveLoadUi's; calling the named public method is the
	// correct altitude.
	[Token(Token = "0x6000323")]
	[Address(RVA = "0x35AE50", Offset = "0x359850", VA = "0x18035AE50")]
	public void OnEndToDismiss()
	{
		foreach (SaveLoadPanel panel in _panels.Values)
		{
			panel.Dismiss();
		}
	}

	[Token(Token = "0x6000324")]
	[Address(RVA = "0x35B090", Offset = "0x359A90", VA = "0x18035B090")]
	public void ResetLoadingSlotCount()
	{
	}

	[Token(Token = "0x6000325")]
	[Address(RVA = "0x35B0F0", Offset = "0x359AF0", VA = "0x18035B0F0", Slot = "92")]
	protected override GameObject FindFocusObject()
	{
		return null;
	}

	[Token(Token = "0x6000326")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	public SaveLoadUi()
	{
	}
}
