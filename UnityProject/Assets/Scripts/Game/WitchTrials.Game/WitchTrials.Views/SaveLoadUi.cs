using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace WitchTrials.Views
{
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
    			// RECONSTRUCTED | Confidence: A | RVA: 0x331650 (shared 4-byte field getter, +0x10 `_mode`)
    			[Token(Token = "0x6000328")]
    			[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
    			get
    			{
    				return _mode;
    			}
    		}

    		[Token(Token = "0x17000069")]
    		public SaveLoadPanel Panel
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (shared generic reference-field getter, +0x18 `_panel`)
    			[Token(Token = "0x6000329")]
    			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    			get
    			{
    				return _panel;
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
    		// Shares the getter body 0x359350 (+0x168 read, folded) with `GetLastLoadMode`.
    		[Token(Token = "0x600030C")]
    		[Address(RVA = "0x359350", Offset = "0x357D50", VA = "0x180359350", Slot = "95")]
    		get
    		{
    			return _presentationMode;
    		}
    		// RECONSTRUCTED | Confidence: A (behavior), B (forwarding spelling) | RVA: 0x359360, 308 B
    		// The same native body as `SetPresentationMode` (dump.cs lists both names
    		// for 0x359360). Whether the setter forwards to it or duplicates it cannot
    		// be told from the bytes.
    		[Token(Token = "0x600030D")]
    		[Address(RVA = "0x359360", Offset = "0x357D60", VA = "0x180359360", Slot = "96")]
    		set
    		{
    			SetPresentationMode(value);
    		}
    	}

    	[Token(Token = "0x17000060")]
    	private GigaCreation.Essentials.Input.IInputManager InputManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3594A0, 202 B
    		// A lazy cache in +0x130, filled with `ServiceLocator.Get<IInputManager>(true)` (`cl = 1`).
    		[Token(Token = "0x600030E")]
    		[Address(RVA = "0x3594A0", Offset = "0x357EA0", VA = "0x1803594A0")]
    		get
    		{
    			return _inputManager ??= GigaCreation.Tools.Service.ServiceLocator.Get<GigaCreation.Essentials.Input.IInputManager>(true);
    		}
    	}

    	[Token(Token = "0x17000061")]
    	private IOverlayFader OverlayFader
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x359570, 202 B
    		// A lazy cache in +0x138, filled with `ServiceLocator.Get<IOverlayFader>(true)`.
    		[Token(Token = "0x600030F")]
    		[Address(RVA = "0x359570", Offset = "0x357F70", VA = "0x180359570")]
    		get
    		{
    			return _overlayFader ??= GigaCreation.Tools.Service.ServiceLocator.Get<IOverlayFader>(true);
    		}
    	}

    	[Token(Token = "0x17000062")]
    	private IDialogProvider DialogProvider
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x359640, 202 B
    		// A lazy cache in +0x140 (typed IDialogProvider). The method metadata is
    		// `ServiceLocator.Get<IDialogManager>(true)`: the service is looked up as
    		// the manager interface, and the result is stored as the provider.
    		[Token(Token = "0x6000310")]
    		[Address(RVA = "0x359640", Offset = "0x358040", VA = "0x180359640")]
    		get
    		{
    			return _dialogProvider ??= GigaCreation.Tools.Service.ServiceLocator.Get<IDialogManager>(true);
    		}
    	}

    	[Token(Token = "0x17000063")]
    	private IWitchTrialsNaninovelManager WitchTrialsNaninovelManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x359710, 202 B
    		// Lazy cache in +0x148 via `ServiceLocator.Get<IWitchTrialsNaninovelManager>()`
    		// with the default `includeDerivedType` (`xor ecx, ecx`).
    		[Token(Token = "0x6000311")]
    		[Address(RVA = "0x359710", Offset = "0x358110", VA = "0x180359710")]
    		get
    		{
    			return _witchTrialsNaninovelManager ??= GigaCreation.Tools.Service.ServiceLocator.Get<IWitchTrialsNaninovelManager>();
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
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3598B0, 202 B
    		// A lazy cache in +0x158, filled with `Engine.GetServiceOrErr<IAudioManager>()`.
    		[Token(Token = "0x6000313")]
    		[Address(RVA = "0x3598B0", Offset = "0x3582B0", VA = "0x1803598B0")]
    		get
    		{
    			return _naniAudioManager ??= Engine.GetServiceOrErr<IAudioManager>();
    		}
    	}

    	[Token(Token = "0x17000066")]
    	private IUIManager NaniUiManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x359980, 202 B
    		// A lazy cache in +0x160, filled with `Engine.GetServiceOrErr<IUIManager>()`.
    		[Token(Token = "0x6000314")]
    		[Address(RVA = "0x359980", Offset = "0x358380", VA = "0x180359980")]
    		get
    		{
    			return _naniUiManager ??= Engine.GetServiceOrErr<IUIManager>();
    		}
    	}

    	[Token(Token = "0x17000067")]
    	private ISaveSlotManager<GameStateMap> SlotManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x359A50, 87 B
    		// Calls `get_NaniStateManager` (0x3597E0, not inlined), then tail-calls
    		// `sub_22E0` with ecx = 0x13 = IStateManager slot 19 `get_GameSlotManager`.
    		// It has no cache field.
    		[Token(Token = "0x6000315")]
    		[Address(RVA = "0x359A50", Offset = "0x358450", VA = "0x180359A50")]
    		get
    		{
    			return NaniStateManager.GameSlotManager;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x359AB0, 1073 B
    	// - `_panels` (+0x170) =
    	//   `Enumerable.ToDictionary(_panelReferences (+0xE8), <>c.<Awake>b__41_0,
    	//   <>c.<Awake>b__41_1)`. Both lambdas are cached in the static `<>c` slots
    	//   +8/+0x10. Their bodies are folded with shared field getters: +0x10
    	//   (int-sized `_mode`, read via `Mode`) and +0x18 (`_panel`, read via `Panel`).
    	// - The inlined `SetPresentationMode(SaveLoadUIPresentationMode.Load)` (value
    	//   1, same ContainsKey / LogWarning shape as 0x359360).
    	// - Tail jump to `CustomUI.Awake`, i.e. `base.Awake()` last.
    	[Token(Token = "0x6000316")]
    	[Address(RVA = "0x359AB0", Offset = "0x3584B0", VA = "0x180359AB0", Slot = "4")]
    	protected override void Awake()
    	{
    		_panels = _panelReferences.ToDictionary(reference => reference.Mode, reference => reference.Panel);
    		SetPresentationMode(SaveLoadUIPresentationMode.Load);
    		base.Awake();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x359EF0, 281 B (entry) + MoveNext 0x35EFC0
    	// For each panel in turn, Save (2) then Load (1):
    	// - `_panels[mode]`, then +0x28 `Grid` (the `get_Grid` getter inlined).
    	// - `get_NaniStateManager` (called fresh for each panel), then `sub_70C0`
    	//   IEngineService<StateConfiguration> slot 0 `Configuration`, then +0x40
    	//   `SaveSlotLimit`.
    	// - `GameStateSlotsGridExtended.InitializeAsync(limit, onSlotClicked,
    	//   onDeleteClicked = HandleDeleteSlotClicked, loadStateAt = LoadSaveSlotAsync)`.
    	//   `onSlotClicked` is `HandleSaveSlotClicked` for Save and
    	//   `HandleLoadSlotClicked` for Load.
    	// Then `foreach (panel in _panels.Values)` runs the inlined
    	// `PageableGridPanel.Dismiss()` (method metadata present; the +0x40 disposable,
    	// CanvasGroup, and Pagination pokes are Dismiss's own body).
    	// Finally `await UniTask.WhenAll(new[] { saveInit, loadInit })`. There is no
    	// `base.Initialize()` call.
    	[Token(Token = "0x6000317")]
    	[Address(RVA = "0x359EF0", Offset = "0x3588F0", VA = "0x180359EF0", Slot = "83")]
    	[AsyncStateMachine(typeof(_003CInitialize_003Ed__42))]
    	public override async Naninovel.UniTask Initialize()
    	{
    		Naninovel.UniTask saveInitializeTask = _panels[SaveLoadUIPresentationMode.Save].Grid.InitializeAsync(NaniStateManager.Configuration.SaveSlotLimit, HandleSaveSlotClicked, HandleDeleteSlotClicked, LoadSaveSlotAsync);
    		Naninovel.UniTask loadInitializeTask = _panels[SaveLoadUIPresentationMode.Load].Grid.InitializeAsync(NaniStateManager.Configuration.SaveSlotLimit, HandleLoadSlotClicked, HandleDeleteSlotClicked, LoadSaveSlotAsync);
    		foreach (SaveLoadPanel panel in _panels.Values)
    		{
    			panel.Dismiss();
    		}
    		await Naninovel.UniTask.WhenAll(saveInitializeTask, loadInitializeTask);
    	}

    	// Folded with the `PresentationMode` getter (0x359350): returns +0x168.
    	[Token(Token = "0x6000318")]
    	[Address(RVA = "0x359350", Offset = "0x357D50", VA = "0x180359350", Slot = "97")]
    	public SaveLoadUIPresentationMode GetLastLoadMode()
    	{
    		return _presentationMode;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x359360, 308 B (shared with set_PresentationMode)
    	// When `_panels` (+0x170, no null guard) does not `ContainsKey(value)`, it calls
    	// `Debug.LogWarning(string.Format("The panel for {0} does not exists.", (boxed)
    	// value))`. `_presentationMode` (+0x168) is assigned `value` either way.
    	[Token(Token = "0x6000319")]
    	[Address(RVA = "0x359360", Offset = "0x357D60", VA = "0x180359360")]
    	private void SetPresentationMode(SaveLoadUIPresentationMode value)
    	{
    		if (!_panels.ContainsKey(value))
    		{
    			UnityEngine.Debug.LogWarning(string.Format("The panel for {0} does not exists.", value));
    		}
    		_presentationMode = value;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x35A010, 419 B
    	// `MonoBehaviour.destroyCancellationToken`, then the inlined
    	// `HandleSaveSlotClickedAsync` kickoff (same bytes as 0x35A1C0), then a tail jump to
    	// `Naninovel.UniTaskExtensions.ForgetCore`, i.e. the inlined `.Forget()`.
    	[Token(Token = "0x600031A")]
    	[Address(RVA = "0x35A010", Offset = "0x358A10", VA = "0x18035A010")]
    	private void HandleSaveSlotClicked(int slotNumber)
    	{
    		HandleSaveSlotClickedAsync(slotNumber, destroyCancellationToken).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A (behavior), B (using-declaration vs using-block spelling) | RVA: 0x35A1C0, 394 B (entry) + MoveNext 0x35D830
    	// Hoisted fields:
    	//   +0x30 slotId, +0x38 blocker (INaninovelInteractionBlocker), +0x40 linkedCts,
    	//   +0x48 / +0x50 the two unnamed usings, +0x58 saveGameTask, +0x68 dialog.
    	// Steps:
    	// 1. `InputManager` slot 0 `PlayResponseSfx(ResponseKind.Submit)`. This comes
    	//    first, before the slot id is computed.
    	// 2. `slotId` = `NaniStateManager.Configuration.IndexToSaveSlotId(slotNumber)`.
    	// 3. `using` the blocker `new NaninovelInteractionBlocker(_naninovelScreen)`.
    	// 4. When `SlotManager.SaveSlotExists(slotId)`, use `DialogProvider.GetOrAddDialog()`:
    	//    - `blocker.SetSortingOrder(dialog.SortingOrder - 1)`.
    	//    - Await `ConfirmAsync(_overwriteSaveSlotDialogData (+0x120), null, ct)`
    	//      (state 0).
    	//    - A `Cancel` result returns from inside the dialog using.
    	// 5. `NaniAudioManager.PlaySfx(_saveSfxPath (+0x110)).Forget()`.
    	// 6. `linkedCts` = `CancellationTokenSource.CreateLinkedTokenSource(new[] { ct })`.
    	// 7. `using` Cysharp `linkedCts.CancelAfterSlim(TimeSpan.FromSeconds(_saveGameTimeout
    	//    (+0x108)), DelayType.DeltaTime (0), PlayerLoopTiming.Update (8))`.
    	// 8. `using` `NaninovelLoadingScreen.Show(LoadingMode.Short, true)`.
    	// 9. `saveGameTask` = `NaniStateManager` slot 28 `SaveGame(slotId)`. While
    	//    `!saveGameTask.IsCompleted`, await `Naninovel.UniTask.Yield(PlayerLoopTiming.Update,
    	//    linkedCts.Token)` (state 1). That awaiter's GetResult is
    	//    `AsyncToken.ThrowIfCanceled`, so the save timeout surfaces as a cancellation.
    	// 10. `state` = await `saveGameTask` (state 2).
    	// 11. `foreach (panel in _panels.Values) panel.Grid.BindSlot(slotNumber, state)`.
    	// Disposal order is loading screen, then CancelAfter, then linkedCts, then blocker.
    	[Token(Token = "0x600031B")]
    	[Address(RVA = "0x35A1C0", Offset = "0x358BC0", VA = "0x18035A1C0")]
    	[AsyncStateMachine(typeof(_003CHandleSaveSlotClickedAsync_003Ed__46))]
    	private async Naninovel.UniTask HandleSaveSlotClickedAsync(int slotNumber, [Optional] CancellationToken ct)
    	{
    		InputManager.PlayResponseSfx(ResponseKind.Submit);
    		string slotId = NaniStateManager.Configuration.IndexToSaveSlotId(slotNumber);
    		using INaninovelInteractionBlocker blocker = new NaninovelInteractionBlocker(_naninovelScreen);
    		if (SlotManager.SaveSlotExists(slotId))
    		{
    			using ProvidableDialog dialog = DialogProvider.GetOrAddDialog();
    			blocker.SetSortingOrder(dialog.SortingOrder - 1);
    			if (await dialog.ConfirmAsync(_overwriteSaveSlotDialogData, null, ct) == ResponseKind.Cancel)
    			{
    				return;
    			}
    		}
    		NaniAudioManager.PlaySfx(_saveSfxPath).Forget();
    		using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
    		using (linkedCts.CancelAfterSlim(TimeSpan.FromSeconds(_saveGameTimeout)))
    		using (NaninovelLoadingScreen.Show(LoadingMode.Short, true))
    		{
    			Naninovel.UniTask<GameStateMap> saveGameTask = NaniStateManager.SaveGame(slotId);
    			while (!saveGameTask.IsCompleted)
    			{
    				await Naninovel.UniTask.Yield(Naninovel.PlayerLoopTiming.Update, linkedCts.Token);
    			}
    			GameStateMap state = await saveGameTask;
    			foreach (SaveLoadPanel panel in _panels.Values)
    			{
    				panel.Grid.BindSlot(slotNumber, state);
    			}
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x35A350, 435 B
    	// `destroyCancellationToken`, the inlined `HandleLoadSlotClickedAsync` kickoff,
    	// then a tail jump to `UniTaskExtensions.ForgetCore`.
    	[Token(Token = "0x600031C")]
    	[Address(RVA = "0x35A350", Offset = "0x358D50", VA = "0x18035A350")]
    	private void HandleLoadSlotClicked(int slotNumber)
    	{
    		HandleLoadSlotClickedAsync(slotNumber, destroyCancellationToken).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A (behavior), B (TryGetUI / using spellings) | RVA: 0x35A510, 410 B (entry) + MoveNext 0x35C490
    	// - `slotId` (hoisted +0x30) = `NaniStateManager.Configuration.IndexToSaveSlotId(slotNumber)`.
    	// - `SlotManager` (the same get_NaniStateManager + IStateManager slot 19 bytes)
    	//   `.SaveSlotExists(slotId)`; when false, return.
    	// - `NaniAudioManager` (called) slot 19 `PlaySfx(_loadSfxPath (+0x118), 1f
    	//   (float at 0x2AE2820), 0f, false, null, default)`, then Naninovel
    	//   `ForgetCore`.
    	// - `using` `new NaninovelInteractionBlocker(_naninovelScreen (+0xE0))`,
    	//   hoisted +0x38 and disposed when state < 0:
    	//   1. Await (Cysharp) `OverlayFader` slot 2 `FadeOutAsync(_fadeOutDurationOnLoad
    	//      (+0xF0), new Color?(_fadeColorOnLoad (+0xF8)), ct)`.
    	//   2. `_naninovelScreen.DismissAsync(new float?(0f), ct).Forget()`, with the
    	//      Nullable ctor called with 0.
    	//   3. The lazy `NaniUiManager` getter (inlined) and `UIManagerExtensions
    	//      .TryGetUI<ITitleUI>` (inlined: GetUI when the manager is non-null).
    	//      When the UI is found and IManagedUI slot 2 `Visible` is true:
    	//      `ManagedUiExtensions.DismissAsync(titleUi, new float?(0f), ct).Forget()`.
    	//   4. The lazy `WitchTrialsNaninovelManager` getter (inlined) slot 3
    	//      `SetTitleScreenShown(false)`.
    	//   5. Await `Naninovel.UniTask.Delay(TimeSpan.FromSeconds(_loadDelay (+0x10C)),
    	//      false, PlayerLoopTiming.Update (8), ct)`.
    	//   6. Await `NaniStateManager` slot 30 `LoadGame(slotId)`.
    	//   7. Await `OverlayFader` slot 4 `FadeInAsync(_fadeInDurationOnLoad (+0xF4),
    	//      new Color?(_fadeColorOnLoad), ct)`.
    	//   8. `Engine.GetServiceOrErr<Naninovel.IInputManager>()` slot 3
    	//      `ProcessInput = true`.
    	[Token(Token = "0x600031D")]
    	[Address(RVA = "0x35A510", Offset = "0x358F10", VA = "0x18035A510")]
    	[AsyncStateMachine(typeof(_003CHandleLoadSlotClickedAsync_003Ed__48))]
    	private async Naninovel.UniTask HandleLoadSlotClickedAsync(int slotNumber, [Optional] CancellationToken ct)
    	{
    		string slotId = NaniStateManager.Configuration.IndexToSaveSlotId(slotNumber);
    		if (!SlotManager.SaveSlotExists(slotId))
    		{
    			return;
    		}
    		NaniAudioManager.PlaySfx(_loadSfxPath).Forget();
    		using (new NaninovelInteractionBlocker(_naninovelScreen))
    		{
    			await OverlayFader.FadeOutAsync(_fadeOutDurationOnLoad, _fadeColorOnLoad, ct);
    			_naninovelScreen.DismissAsync(0f, ct).Forget();
    			if (NaniUiManager.TryGetUI<ITitleUI>(out ITitleUI titleUi) && titleUi.Visible)
    			{
    				titleUi.DismissAsync(0f, ct).Forget();
    			}
    			WitchTrialsNaninovelManager.SetTitleScreenShown(false);
    			await Naninovel.UniTask.Delay(TimeSpan.FromSeconds(_loadDelay), false, Naninovel.PlayerLoopTiming.Update, ct);
    			await NaniStateManager.LoadGame(slotId);
    			await OverlayFader.FadeInAsync(_fadeInDurationOnLoad, _fadeColorOnLoad, ct);
    			Engine.GetServiceOrErr<Naninovel.IInputManager>().ProcessInput = true;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x35A6B0, 419 B
    	// `destroyCancellationToken`, the inlined `HandleDeleteSlotClickedAsync` kickoff,
    	// then a tail jump to `UniTaskExtensions.ForgetCore`.
    	[Token(Token = "0x600031E")]
    	[Address(RVA = "0x35A6B0", Offset = "0x3590B0", VA = "0x18035A6B0")]
    	private void HandleDeleteSlotClicked(int slotNumber)
    	{
    		HandleDeleteSlotClickedAsync(slotNumber, destroyCancellationToken).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x35A860, 410 B (entry) + MoveNext 0x35B8F0
    	// - `slotId` (hoisted +0x30) = `NaniStateManager.Configuration.IndexToSaveSlotId(slotNumber)`.
    	// - `SlotManager.SaveSlotExists(slotId)` (inlined getter bytes); when false, return.
    	// - `InputManager` (GigaCreation) slot 0 `PlayResponseSfx(ResponseKind.Submit (1))`.
    	// - `using INaninovelInteractionBlocker blocker = new NaninovelInteractionBlocker(_naninovelScreen)`
    	//   (hoisted +0x38, typed as the interface):
    	//   - `using ProvidableDialog dialog = DialogProvider` IDialogProvider slot 0
    	//     `GetOrAddDialog()` (via `sub_5DA0`, hoisted +0x40):
    	//     - `blocker` slot 0 `SetSortingOrder(dialog.SortingOrder - 1)`.
    	//     - Await (Cysharp) `dialog.ConfirmAsync(_deleteSaveSlotDialogData
    	//       (+0x128), null, ct)`.
    	//     - A result of `ResponseKind.Cancel` (0) returns from **inside** the
    	//       dialog using: that path disposes both without clearing +0x40.
    	//   - Otherwise the dialog is disposed, +0x40 is cleared, and then:
    	//     - `SlotManager` ISaveSlotManager slot 20 `DeleteSaveSlot(slotId)`.
    	//     - `foreach (panel in _panels.Values) panel.Grid.BindSlot(slotNumber, null)`.
    	[Token(Token = "0x600031F")]
    	[Address(RVA = "0x35A860", Offset = "0x359260", VA = "0x18035A860")]
    	[AsyncStateMachine(typeof(_003CHandleDeleteSlotClickedAsync_003Ed__50))]
    	private async Naninovel.UniTask HandleDeleteSlotClickedAsync(int slotNumber, [Optional] CancellationToken ct)
    	{
    		string slotId = NaniStateManager.Configuration.IndexToSaveSlotId(slotNumber);
    		if (!SlotManager.SaveSlotExists(slotId))
    		{
    			return;
    		}
    		InputManager.PlayResponseSfx(ResponseKind.Submit);
    		using INaninovelInteractionBlocker blocker = new NaninovelInteractionBlocker(_naninovelScreen);
    		using (ProvidableDialog dialog = DialogProvider.GetOrAddDialog())
    		{
    			blocker.SetSortingOrder(dialog.SortingOrder - 1);
    			if (await dialog.ConfirmAsync(_deleteSaveSlotDialogData, null, ct) == ResponseKind.Cancel)
    			{
    				return;
    			}
    		}
    		SlotManager.DeleteSaveSlot(slotId);
    		foreach (SaveLoadPanel panel in _panels.Values)
    		{
    			panel.Grid.BindSlot(slotNumber, null);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x35AA00, 417 B (entry) + MoveNext 0x35F870
    	// - `slotId` = `NaniStateManager.Configuration.IndexToSaveSlotId(slotNumber)`.
    	//   It is an unhoisted local, since it is only used before the first await.
    	// - `SlotManager` then `sub_3140` ecx = 0x12 = ISaveSlotManager slot 18
    	//   `SaveSlotExists(slotId)`; when false, SetResult(null).
    	// - When `_loadingSlotCount` (+0x178) is null: await
    	//   `ISaveSlotManager<GameStateMap>` slot 1 `Load(slotId)` (via `sub_7000`) and
    	//   return the result (state 0).
    	// - Otherwise `_loadingSlotCount.Value++` (get_Value into a local, then
    	//   set_Value + 1), and the same await (state 1) runs inside try/finally.
    	//   The finally helper 0x35FE20 runs only when the state is not suspended
    	//   and does `_loadingSlotCount.Value--`, with no null guard (NRE if cleared
    	//   meanwhile).
    	// The counter drives the pagination loading screen in OnShow's subscription.
    	[Token(Token = "0x6000320")]
    	[Address(RVA = "0x35AA00", Offset = "0x359400", VA = "0x18035AA00")]
    	[AsyncStateMachine(typeof(_003CLoadSaveSlotAsync_003Ed__51))]
    	private async Naninovel.UniTask<GameStateMap> LoadSaveSlotAsync(int slotNumber)
    	{
    		string slotId = NaniStateManager.Configuration.IndexToSaveSlotId(slotNumber);
    		if (!SlotManager.SaveSlotExists(slotId))
    		{
    			return null;
    		}
    		if (_loadingSlotCount == null)
    		{
    			return await SlotManager.Load(slotId);
    		}
    		_loadingSlotCount.Value++;
    		try
    		{
    			return await SlotManager.Load(slotId);
    		}
    		finally
    		{
    			_loadingSlotCount.Value--;
    		}
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
    	//
    	// RECONSTRUCTED | Confidence: A (behavior), B (two sequential `if`s spelling) | RVA: 0x35B6C0, 395 B
    	// The subscribed lambda `<OnShow>b__52_0(int cnt)` is an instance method,
    	// since it captures `this`. The previous Confidence D placeholder
    	// `OnShowSlotCountChanged` is replaced by the lambda written inline.
    	// - Vtable +0x2F8 = ScriptableUIBehaviour slot 28 `get_Visible`.
    	// - When visible, `_loadingScreenOnPaginate` (+0x180) is null, and `cnt > 0`:
    	//   `NaninovelLoadingScreen.Show(LoadingMode.Short (1), haltInput: true,
    	//   params [ _naninovelScreen (+0xE0) ])`. The one-element
    	//   ICancelEventBlockable[] has the covariant array-store type check, and the
    	//   result is stored in the field.
    	// - Then, independently: when the field is non-null and `cnt <= 0`,
    	//   `IDisposable.Dispose()` and the field is cleared.
    	// The loading overlay therefore stays up while any slot page is loading
    	// (see `LoadSaveSlotAsync`'s counter).
    	[Token(Token = "0x6000321")]
    	[Address(RVA = "0x35ABB0", Offset = "0x3595B0", VA = "0x18035ABB0")]
    	public void OnShow()
    	{
    		_loadingSlotCount?.Dispose();
    		_loadingSlotCount = new ReactiveProperty<int>(0);
    		_loadingSlotCount.Subscribe(cnt =>
    		{
    			if (Visible && _loadingScreenOnPaginate == null && cnt > 0)
    			{
    				_loadingScreenOnPaginate = NaninovelLoadingScreen.Show(LoadingMode.Short, true, _naninovelScreen);
    			}
    			if (_loadingScreenOnPaginate != null && cnt <= 0)
    			{
    				_loadingScreenOnPaginate.Dispose();
    				_loadingScreenOnPaginate = null;
    			}
    		}).AddTo(this);
    		_panels[_presentationMode].Present();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x35ADB0, 152 B
    	// When +0x178 `_loadingSlotCount` is non-null, `ReactiveProperty<int>.Dispose()`
    	// runs; then the field is cleared to null unconditionally.
    	[Token(Token = "0x6000322")]
    	[Address(RVA = "0x35ADB0", Offset = "0x3597B0", VA = "0x18035ADB0")]
    	public void OnHide()
    	{
    		_loadingSlotCount?.Dispose();
    		_loadingSlotCount = null;
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

    	// RECONSTRUCTED | Confidence: A | RVA: 0x35B090, 90 B
    	// When +0x178 `_loadingSlotCount` is non-null, `ReactiveProperty<int>.set_Value(0)`.
    	[Token(Token = "0x6000324")]
    	[Address(RVA = "0x35B090", Offset = "0x359A90", VA = "0x18035B090")]
    	public void ResetLoadingSlotCount()
    	{
    		if (_loadingSlotCount != null)
    		{
    			_loadingSlotCount.Value = 0;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (behavior), B (Unity bool-vs-`== null` spelling) | RVA: 0x35B0F0, 1479 B
    	// - `grid` = `_panels[_presentationMode]` (+0x170 / +0x168), then +0x28
    	//   `Grid`. A null or destroyed grid (`m_CachedPtr` == 0) returns null.
    	// - Vtable +0x538 = ScriptableGrid slot 64 `get_Slots`, called separately
    	//   for the null check, for `IReadOnlyCollection.Count` (0 returns null), and
    	//   for the `foreach` (GetEnumerator / MoveNext / Current, then Dispose in
    	//   finally).
    	// - Per slot: `slot.gameObject` followed by the cached icall
    	//   "UnityEngine.GameObject::get_activeInHierarchy_Injected" (PE string at
    	//   0x2605870). Inactive slots are skipped.
    	//   - When `best` is not alive, `best = slot`.
    	//   - Otherwise vtable +0x618 = GameStateSlot slot 78 `get_LastSelectTime`
    	//     on both; `comiss` / `jbe` means strictly greater replaces `best`, and
    	//     NaN does not.
    	// - Returns `best.gameObject` when `best` is alive, else null. The result is
    	//   the most recently selected visible slot on the current page.
    	[Token(Token = "0x6000325")]
    	[Address(RVA = "0x35B0F0", Offset = "0x359AF0", VA = "0x18035B0F0", Slot = "92")]
    	protected override GameObject FindFocusObject()
    	{
    		GameStateSlotsGridExtended grid = _panels[_presentationMode].Grid;
    		if (!grid)
    		{
    			return null;
    		}
    		if (grid.Slots == null || grid.Slots.Count == 0)
    		{
    			return null;
    		}
    		GameStateSlotExtended focusSlot = null;
    		foreach (GameStateSlotExtended slot in grid.Slots)
    		{
    			if (slot.gameObject.activeInHierarchy && (!focusSlot || slot.LastSelectTime > focusSlot.LastSelectTime))
    			{
    				focusSlot = slot;
    			}
    		}
    		return focusSlot ? focusSlot.gameObject : null;
    	}

    	[Token(Token = "0x6000326")]
    	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
    	public SaveLoadUi()
    	{
    	}
    }
}
